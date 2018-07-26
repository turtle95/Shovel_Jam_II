using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	//public bool isNight = false;
	public Light sun;
    public float startIntensity = 4f;
    GameObject[] glowies;

    variableTracker varTrack;
   

    EventManager eScript;
    float dayNightTimer = 60f;
    float dayLength = 60f;
    float speed = 6f;

    public Image dayNightSlider;
   
    public float nightTimer = 0;
    public Text announcerText;
  //  bool triggered = true;

    public RotateLava lavaScript;

    //stuff to switch on/off during daynight switch
    public GameObject[] nightOn;
    public GameObject[] nightOff;
    public GameObject[] dayOn;
    public GameObject[] dayOff;


	void Start() {
        dayNightTimer = dayLength;
		//sun = gameObject.GetComponent<Light>();
        eScript = GameObject.Find("EventManager").GetComponent<EventManager>();
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        glowies = GameObject.FindGameObjectsWithTag("Glowy");
        for (int i = 0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(true);
        }
    }

    private void Update()
    {
        if (varTrack.isNight)
        {
            sun.intensity = startIntensity * (nightTimer / dayLength);

            nightTimer += Time.deltaTime * speed;
            //if (!triggered)
            //    dayNightTimer -= Time.deltaTime * speed;
            //else
            //    dayNightTimer += Time.deltaTime * speed;

            //if (dayNightTimer < 40)
            //{
            //    for (int i = 0; i < glowies.Length; i++)
            //    {
            //        glowies[i].SetActive(false);
            //    }
            //}
        }
        
        else
        {
            sun.intensity = startIntensity * (dayNightTimer / dayLength);

            //if (dayNightTimer < 40)
            //{
            //    for (int i = 0; i < glowies.Length; i++)
            //    {
            //        glowies[i].SetActive(true);
            //    }

            //}
        }
     
        dayNightTimer -= Time.deltaTime * speed;


        dayNightSlider.fillAmount = (dayNightTimer / dayLength);

        //if (dayNightTimer <= 0)
         //   triggered = true;
        
        DynamicGI.UpdateEnvironment();
        if (dayNightTimer <= 10f)
        {
            if (varTrack.isNight)
                ChangeToMorning();
            else
                ChangeToNight();
        }
    }

    

    public void ChangeToNight () {
       
        for (int i = 0; i < nightOn.Length; i++)
        {
            nightOn[i].SetActive(true);
        }
        for (int i = 0; i < nightOff.Length; i++)
        {
            nightOff[i].SetActive(false);
        }

        nightTimer = 0;
        dayNightTimer = dayLength;
        //announcer.SetActive(true);
        announcerText.text = "Night";
        varTrack.isNight = true;
        // sun.enabled = false;
       

        //Triggers events based on what is turned on/off
        if (varTrack.eventOne)
            eScript.EventOne();
        if (varTrack.eventTwo)
        {
            //eScript.EventTwo();
            lavaScript.enabled = true;
            lavaScript.speed = 8f;
        }
        if (varTrack.eventThree)
            eScript.EventThree();

       // DynamicGI.UpdateEnvironment();
  
        
        
	}

	public void ChangeToMorning () {

        for (int i = 0; i < dayOn.Length; i++)
        {
            dayOn[i].SetActive(true);
        }
        for (int i = 0; i < dayOff.Length; i++)
        {
            dayOff[i].SetActive(false);
        }

        if (varTrack.eventTwo)
            lavaScript.speed = 3f;
        dayNightTimer = dayLength;
       // announcer.SetActive(true);
        announcerText.text = "Morning";
        varTrack.isNight = false;
        // sun.enabled = true;
        //for (int i = 0; i < glowies.Length; i++)
        //{
        //    glowies[i].SetActive(false);
        //}
        //DynamicGI.UpdateEnvironment();



    }
}
