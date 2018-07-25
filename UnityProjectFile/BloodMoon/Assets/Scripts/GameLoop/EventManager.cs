using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    //public Text announcer;


    public GameObject[] TurnOn;
    public GameObject[] TurnOff;

    public void EventOne()
    {

       

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
