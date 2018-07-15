using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableTracker : MonoBehaviour {


    public bool controller = false;
    public GameObject pauseMenu;

    public float stamina = 10;
    public float health = 10;
    public float speed = 1;

    public float maxStam = 100;
    public float maxHealth = 100;
    public float maxSpeed = 20;

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
