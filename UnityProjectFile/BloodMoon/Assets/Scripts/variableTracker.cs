using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableTracker : MonoBehaviour {


    public bool controller = false;
    public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
}
