using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderShrine : MonoBehaviour {


    variableTracker varTrack;
    public GameObject announcer; //used to announce day/night status and event status...only does it for the water appearing though
    Text announcerText;
    public GameObject dayModel;
    public GameObject nightModel;
    EventManager eScript;

    public AudioSource aSource;
    public AudioClip aClip;
	
	void Start () {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        eScript = GameObject.Find("EventManager").GetComponent<EventManager>();
        announcerText = announcer.GetComponent<Text>();
	}

    private void Update()
    {
        //switches models based on day/night so that you know when you can/can't shoot it
            if (varTrack.isNight)
            {
                dayModel.SetActive(false);
                nightModel.SetActive(true);
            }
            else
            {
                dayModel.SetActive(true);
                nightModel.SetActive(false);
            }
        
    }

    //if shot during the day it triggers event two
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !varTrack.isNight && varTrack.eventOne)
        {
            aSource.PlayOneShot(aClip);
            varTrack.eventOne = false;
            varTrack.eventTwo = true;
          
            eScript.EventTwo();
            announcer.SetActive(true);
            announcerText.text = "Spider nest flooded";
            Destroy(this.gameObject);
        }
    }
}
