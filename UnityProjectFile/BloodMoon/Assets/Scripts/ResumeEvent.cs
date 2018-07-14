using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeEvent : MonoBehaviour {

	public void Resume(GameObject pauseMenu)
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
