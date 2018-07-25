using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	public bool isNight = false;
	Light sun;
    public float startIntensity = 4f;
    GameObject[] glowies;

    variableTracker varTrack;
    public GameObject waterRise;
    public bool dieOnNight = false;

    public GameObject announcer;
    public GameObject endGame;

    EventManager eScript;
    float dayNightTimer = 60f;
    float dayLength = 60f;
    float speed = 6f;

    public Image dayNightSlider;
    public GameObject daySymbol;
    public GameObject nightSymbol;
    public float nightTimer = 0;
    public Text announcerText;
    bool triggered = true;

	void Start() {
        dayNightTimer = dayLength;
		sun = gameObject.GetComponent<Light>();
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
        if (isNight)
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
            if (isNight)
                ChangeToMorning();
            else
                ChangeToNight();
        }
    }

    

    public void ChangeToNight () {
        nightTimer = 0;
        //triggered = false;
        daySymbol.SetActive(false);
        nightSymbol.SetActive(true);
        dayNightTimer = dayLength;
        announcer.SetActive(true);
        announcerText.text = "Night";
        isNight = true;
        // sun.enabled = false;
       

        //Triggers events based on what is turned on/off
        if (varTrack.eventOne)
            eScript.EventOne();
        if (varTrack.eventTwo)
            eScript.EventTwo();
        if (varTrack.eventThree)
            eScript.EventThree();

       // DynamicGI.UpdateEnvironment();
  
        
        
	}

	public void ChangeToMorning () {
       // triggered = false;
        daySymbol.SetActive(true);
        nightSymbol.SetActive(false);
        dayNightTimer = dayLength;
        announcer.SetActive(true);
        announcerText.text = "Morning";
        isNight = false;
        // sun.enabled = true;
        //for (int i = 0; i < glowies.Length; i++)
        //{
        //    glowies[i].SetActive(false);
        //}
        //DynamicGI.UpdateEnvironment();



    }
}
