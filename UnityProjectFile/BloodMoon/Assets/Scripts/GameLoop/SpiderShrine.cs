using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderShrine : MonoBehaviour {


    variableTracker varTrack;
    public GameObject announcer;
    Text announcerText;
    public GameObject dayModel;
    public GameObject nightModel;
    EventManager eScript;
    //public GameObject bloodLake;

	// Use this for initialization
	void Start () {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        eScript = GameObject.Find("EventManager").GetComponent<EventManager>();
        announcerText = announcer.GetComponent<Text>();
	}

    private void Update()
    {
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !varTrack.isNight && varTrack.eventOne)
        {
            
            varTrack.eventOne = false;
            varTrack.eventTwo = true;
           // bloodLake.SetActive(true);
            eScript.EventTwo();
            announcer.SetActive(true);
            announcerText.text = "Spider nest flooded";
            Destroy(this.gameObject);
        }
    }
}
