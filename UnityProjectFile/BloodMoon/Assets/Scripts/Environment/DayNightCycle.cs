﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	
	public Light sun;
    public float startIntensity = 4f;
    GameObject[] glowies;

    variableTracker varTrack;
   

    EventManager eScript;
    float dayNightTimer = 60f;
    float dayLength = 60f;
    float speed = 2f;

    public Image daySlider;
    public Image nightSlider;

    public float nightTimer = 0;
    public Text announcerText;
 

    public RotateLava lavaScript;

    //stuff to switch on/off during daynight switch
    public GameObject[] nightOn;
    public GameObject[] nightOff;
    public GameObject[] dayOn;
    public GameObject[] dayOff;
    public SpiderSpawner sScript;

	void Start() {

        sScript = GetComponent<SpiderSpawner>();
        dayNightTimer = dayLength;
		
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
            nightSlider.fillAmount = (dayNightTimer / dayLength);
        }
        
        else
        {
            sun.intensity = startIntensity * (dayNightTimer / dayLength);
            daySlider.fillAmount = (dayNightTimer / dayLength);
        }
     
        dayNightTimer -= Time.deltaTime * speed;


        
       


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
       
        announcerText.text = "Night";
        varTrack.isNight = true;
       

        //Triggers events based on what is turned on/off
        if (varTrack.eventOne)
            eScript.EventOne();
        if (varTrack.eventTwo)
        {
            
            lavaScript.enabled = true;
            lavaScript.speed = 8f;
        }
        if (varTrack.eventThree)
            eScript.EventThree();
        
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

        if (varTrack.eventOne)
            sScript.ClearSpiders(false);

        if (varTrack.eventTwo)
            lavaScript.speed = 3f;
        dayNightTimer = dayLength;
        announcerText.text = "Morning";
        varTrack.isNight = false;

    }
}
