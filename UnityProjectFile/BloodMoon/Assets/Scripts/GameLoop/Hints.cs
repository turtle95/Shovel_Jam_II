using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour {

    public GameObject hints;
    public GameObject[] messages;

    public int currentHint = 0;

    variableTracker varTrack;

    private void Start()
    {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
    }

    // Update is called once per frame
    void Update () {


        if (varTrack.eventTwo)
            currentHint = 2;
        if (varTrack.eventThree)
            currentHint = 3;


        if (Input.GetButtonDown("Hint"))
        {
            Time.timeScale = 0f;
            hints.SetActive(true);
            for(int i=0; i<messages.Length; i++)
            {
                messages[i].SetActive(false);
            }
            messages[currentHint].SetActive(true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 1f;
            hints.SetActive(false);
        }
	}
}
