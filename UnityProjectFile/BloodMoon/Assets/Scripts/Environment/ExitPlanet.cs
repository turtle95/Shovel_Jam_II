using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlanet : MonoBehaviour {

    public GameObject[] turnOn;

    public GameObject[] turnOff;

    public Stage3Movement pScript;
    public Stage3CamGrav cScript;

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

            pScript.outsidePlanet = true;
            cScript.outsidePlanet = true;

            Destroy(this.gameObject);
        }
    }
}
