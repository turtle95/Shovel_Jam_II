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
    public float maxSpeed = 10;
    
    public PlayerHealth hScript;
    public Stage3Movement mScript;

    public GameObject staminaText;
    public GameObject healthText;
    public GameObject speedText;
    public GameObject juicytext;
    public GameObject shovelText;

	// Use this for initialization
	void Start () {
        hScript = GameObject.Find("PlayerPrefab").GetComponent<PlayerHealth>();
        mScript = GameObject.Find("PlayerBox").GetComponent<Stage3Movement>();
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


    public void AddHealth()
    {
        healthText.SetActive(true);
        hScript.currentHealth += 2;
        hScript.healthSlider.maxValue += 1;
    }

    public void AddStamina()
    {
        staminaText.SetActive(true);
        hScript.currentEnergy += 10;
        hScript.energySlider.maxValue += 5;
    }

    public void AddSpeed()
    {
        speedText.SetActive(true);
        mScript.refWalkSpeed += 1;
        mScript.walkSpeed += 1;
        mScript.runSpeed += 1;
        mScript.dashDistance += 1;
    }

   public void GrabWeapon(int chosen)
    {
        switch (chosen)
        {
            case 1: shovelText.SetActive(true);
                break;
            case 2: juicytext.SetActive(true);
                break;
        }
    }
}
