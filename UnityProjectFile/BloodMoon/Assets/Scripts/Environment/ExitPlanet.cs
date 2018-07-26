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

            //reverses the gravity
            pScript.outsidePlanet = true;
            cScript.outsidePlanet = true;

            pScript.walkSpeed *= 4;
            pScript.runSpeed *= 4;
           // pScript.jumpForce *= 2;
            pScript.gravity = -20;

            //StartCoroutine(BurstTimer());

            
        }
    }

    public IEnumerator BurstTimer()
    {
        pScript.flying = true;
        yield return new WaitForSeconds(0.2f);
       
        pScript.flying = false;
        Destroy(this.gameObject);
    }
}
