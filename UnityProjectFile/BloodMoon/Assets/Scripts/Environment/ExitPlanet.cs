using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlanet : MonoBehaviour {

    public GameObject[] turnOn;

    public GameObject[] turnOff;

    public Stage3Movement pScript;
    //public Stage3CamGrav cScript;
    variableTracker varTrack;

    public GameObject seal;
    public DayNightCycle sScript;
    private void Start()
    {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i=0; i<turnOn.Length; i++)
            {
                turnOn[i].SetActive(true);
            }

            for (int i = 0; i < turnOff.Length; i++)
            {
                turnOff[i].SetActive(false);
            }

            //reverses the gravity
            varTrack.outsidePlanet = true;
            //cScript.outsidePlanet = true;


            // pScript.jumpForce *= 2;
            sScript.enabled = false;

            StartCoroutine(BurstTimer());

            
        }
    }

    public IEnumerator BurstTimer()
    {
        pScript.walkSpeed *= 2;
        pScript.runSpeed *= 2;
        pScript.flying = true;
        yield return new WaitForSeconds(0.2f);
        
        pScript.gravity -= 10;
        seal.SetActive(true);
        //pScript.flying = false;
        Destroy(this.gameObject);
    }
}
