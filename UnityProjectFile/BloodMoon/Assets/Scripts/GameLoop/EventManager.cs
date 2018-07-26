using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

   //Not everything is being used in this script yet, waiting until we develop out the events a little more


    public GameObject[] TurnOn;
    public GameObject[] TurnOff;

    public int bossCount = 6;
    public GameObject winScreen;
    public PlayerHealth hScript;
    public GameObject explosion;
    public GameObject planet;
    SpiderSpawner sScript;

    private void Start()
    {
        sScript = GetComponent<SpiderSpawner>();
    }

    private void Update()
    {
        if(bossCount <= 0)
        {
            //end game!
            
            Instantiate(explosion, planet.transform.position, planet.transform.rotation);
            Destroy(planet);

            winScreen.SetActive(true);
            hScript.Win();
        }
    }

    public void EventOne()
    {

       sScript.MassSpawn();

        for(int i=0; i< TurnOn.Length; i++)
        {
            TurnOn[i].SetActive(true);
        }

        for (int i = 0; i < TurnOff.Length; i++)
        {
            TurnOff[i].SetActive(false);
        }
    }

    public GameObject[] TurnOn2;
    public GameObject[] TurnOff2;

    public void EventTwo()
    {
        for (int i = 0; i < TurnOn2.Length; i++)
        {
            TurnOn2[i].SetActive(true);
        }

        for (int i = 0; i < TurnOff2.Length; i++)
        {
            TurnOff2[i].SetActive(false);
        }
    }


    public GameObject[] TurnOn3;
    public GameObject[] TurnOff3;

    public void EventThree()
    {
        for (int i = 0; i < TurnOn3.Length; i++)
        {
            TurnOn3[i].SetActive(true);
        }

        for (int i = 0; i < TurnOff3.Length; i++)
        {
            TurnOff3[i].SetActive(false);
        }
    }
}
