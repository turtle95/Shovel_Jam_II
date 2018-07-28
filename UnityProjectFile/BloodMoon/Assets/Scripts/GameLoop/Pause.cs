using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    bool paused = true;
    public GameObject pauseMenu;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Escape") )
        {
            if (!paused)
            {
                pauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0f;
            }
            else
            {
                Application.Quit();
            }
        }

        if(paused && Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 1.0f;
            paused = false;
            pauseMenu.SetActive(false);           
        }


	}
}
