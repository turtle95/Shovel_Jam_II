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


    public Image dayNightSlider;
    public GameObject daySymbol;
    public GameObject nightSymbol;

	void Start() {
        dayNightTimer = dayLength;
		sun = gameObject.GetComponent<Light>();
        eScript = GameObject.Find("EventManager").GetComponent<EventManager>();
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        glowies = GameObject.FindGameObjectsWithTag("Glowy");
        for (int i = 0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(false);
        }
    }

    private void Update()
    {
        dayNightTimer -= Time.deltaTime;
        sun.intensity = startIntensity * (dayNightTimer / dayLength);
        dayNightSlider.fillAmount = (dayNightTimer / dayLength);

        if(dayNightTimer <= 1)
        {
            if (isNight)
                ChangeToMorning();
            else
                ChangeToNight();
        }
    }

    private void LateUpdate()
    {
        DynamicGI.UpdateEnvironment();
    }

    public void ChangeToNight () {
        daySymbol.SetActive(false);
        nightSymbol.SetActive(true);
        dayNightTimer = dayLength;
        // announcer.SetActive(true);
        isNight = true;
        sun.enabled = false;
        for (int i=0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(true);
        }

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
        daySymbol.SetActive(true);
        nightSymbol.SetActive(false);
        dayNightTimer = dayLength;
        isNight = false;
        sun.enabled = true;
        for (int i = 0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(false);
        }
        //DynamicGI.UpdateEnvironment();

        
        
    }
}
