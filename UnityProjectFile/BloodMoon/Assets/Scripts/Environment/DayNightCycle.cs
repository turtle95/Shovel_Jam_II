using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	public bool isNight = false;
	Light sun;
    GameObject[] glowies;

    variableTracker varTrack;
    public GameObject waterRise;
    public bool dieOnNight = false;


	void Start() {
		sun = gameObject.GetComponent<Light>();
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        glowies = GameObject.FindGameObjectsWithTag("Glowy");
        for (int i = 0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(false);
        }
    }

	//void Update() {
	//	if (isNight) {
	//		sun.enabled = false;
 //           glowies.SetActive(true)
	//	} else {
	//		sun.enabled = true;
	//	}
	//}


	public void ChangeToNight () {
		isNight = true;
        sun.enabled = false;
        for (int i=0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(true);
        }
        
        DynamicGI.UpdateEnvironment();
        varTrack.currentNight++;
        Debug.Log(varTrack.currentNight);
        switch (varTrack.currentNight)
        {
            case 1: break;
            case 3:
                waterRise.SetActive(true);
                break;
            case 5: dieOnNight = true;
                break;
            default: break;
        }
        
	}

	public void ChangeToMorning () {
		isNight = false;
        sun.enabled = true;
        for (int i = 0; i < glowies.Length; i++)
        {
            glowies[i].SetActive(false);
        }
        DynamicGI.UpdateEnvironment();
        varTrack.currentNight++;
        Debug.Log(varTrack.currentNight);
        switch (varTrack.currentNight)
        {
            case 0: break;
            case 2: break;
            case 4:
                waterRise.SetActive(false);
                break;
            default: break;
        }
        
    }
}
