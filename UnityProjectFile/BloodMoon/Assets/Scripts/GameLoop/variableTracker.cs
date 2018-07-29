using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class variableTracker : MonoBehaviour {

    bool inputDecided = false;
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

    public int currentNight = 0;
    public float dayNightTimer = 60;
    public bool isNight = false;

    public bool eventOne = true;
    public bool eventTwo = false;
    public bool eventThree = false;

    public bool outsidePlanet = false;
    public GameObject announcer;
    Text announcerText;
	// Use this for initialization
	void Start () {
        announcerText = announcer.GetComponent<Text>();
        hScript = GameObject.Find("PlayerPrefab").GetComponent<PlayerHealth>();
        mScript = GameObject.Find("PlayerBox").GetComponent<Stage3Movement>();
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.P))
        //    SceneManager.LoadScene(0);

        //if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        //{
        //    Time.timeScale = 1;
        //    pauseMenu.SetActive(false);
        //}

        //else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        //{
        //    Time.timeScale = 0;
        //    pauseMenu.SetActive(true);
        //}

        if (!inputDecided)
            TestForController();
    }

    //adds to your health stats
    public void AddHealth()
    {
        announcer.SetActive(true);
        announcerText.text = "Obtained Ship Parts!";
        healthText.SetActive(true);
        hScript.currentHealth += 2;
        //hScript.healthSlider.maxValue += 1;
    }

    //adds to your stamina stats
    public void AddStamina()
    {
        announcer.SetActive(true);
        announcerText.text = "Obtained Ship Parts!";
        staminaText.SetActive(true);
        hScript.currentEnergy += 10;
       // hScript.energySlider.maxValue += 5;
    }

    //adds to your speed stats
    public void AddSpeed()
    {
        announcer.SetActive(true);
        announcerText.text = "Obtained Ship Parts!";
        speedText.SetActive(true);
        mScript.refWalkSpeed += 5;
        mScript.walkSpeed += 5;
        mScript.runSpeed += 5;
        mScript.dashDistance += 5;
    }

    //Displays text based on which weapon you picked up
   public void GrabWeapon(int chosen)
    {
        announcer.SetActive(true);
        announcerText.text = "Obtained Ship Parts!";
        switch (chosen)
        {
            case 1: shovelText.SetActive(true);
                break;
            case 2: juicytext.SetActive(true);
                break;
        }
    }

    //Checks if a keyboard/mouse button is pressed or a controller button is pressed and sets the sensitivity based on that
    void TestForController()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0) ||
                     Input.GetKey(KeyCode.Joystick1Button1) ||
                     Input.GetKey(KeyCode.Joystick1Button2) ||
                     Input.GetKey(KeyCode.Joystick1Button3) ||
                     Input.GetKey(KeyCode.Joystick1Button4) ||
                     Input.GetKey(KeyCode.Joystick1Button5) ||
                     Input.GetKey(KeyCode.Joystick1Button6) ||
                     Input.GetKey(KeyCode.Joystick1Button7) ||
                     Input.GetKey(KeyCode.Joystick1Button8) ||
                     Input.GetKey(KeyCode.Joystick1Button9) ||
                     Input.GetKey(KeyCode.Joystick1Button10) ||
                     Input.GetKey(KeyCode.Joystick1Button11) ||
                     Input.GetKey(KeyCode.Joystick1Button12) ||
                     Input.GetKey(KeyCode.Joystick1Button13) ||
                     Input.GetKey(KeyCode.Joystick1Button14) ||
                     Input.GetKey(KeyCode.Joystick1Button15) ||
                     Input.GetKey(KeyCode.Joystick1Button16) ||
                     Input.GetKey(KeyCode.Joystick1Button17) ||
                     Input.GetKey(KeyCode.Joystick1Button18) ||
                     Input.GetKey(KeyCode.Joystick1Button19))
        {
            controller = true;
            inputDecided = true;
            Debug.Log("Controller Mode");
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
            Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) 
            || Input.GetKey(KeyCode.LeftArrow))
        {
            controller = false;
            inputDecided = true;
        }

    }


}
