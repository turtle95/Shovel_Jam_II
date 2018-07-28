using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    variableTracker varTrack;

	// Health
    public float startingHealth = 10;                            // The amount of health the player starts the game with.
    public float currentHealth;                                   // The current health the player has.
    public Image healthSlider;                                 // Reference to the UI's health bar.

	// Energy
	public float startingEnergy = 100;
	public float currentEnergy;
	public Image energySlider;

    public AudioSource audManager;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip nightJingle;

    public DayNightCycle sun;

    

	bool isNight;												// Whether it is nighttime.
    bool isDead;                                                // Whether the player is dead.
    public bool damaged;                                               // True when the player gets damaged.

    public int energyGain = 1;
    public Animator screenFlash;
    public GameObject player;
    public GameObject deathPanel;
    Stage3Movement pScript;

    private void Start()
    {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        pScript = player.GetComponent<Stage3Movement>();
    }

    void Awake ()
    {
       

        // Set the initial health and stamina of the player.
        currentHealth = startingHealth;
		currentEnergy = startingEnergy;
    }


    void Update ()
    {
        EnergyManage(energyGain);

        if (isDead && Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(1);
        }
        
		
    }

    public void TakeDamage (int amount)
    {
        if (damaged == false)
        {
            damaged = true;
            
            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            //// Set the health bar's value to the current health.
            healthSlider.fillAmount = currentHealth/startingHealth;

            // Play the hurt sound effect.
            //audManager.PlayOneShot(hurtSound);

           
            if (currentHealth <= 0 && !isDead)
            {
               
                //audManager.PlayOneShot(deathSound);
                Death();
            }

            StartCoroutine(WaitForFlash());
        }
    }

	public void EnergyManage (int amount) 
	{
		energySlider.fillAmount = currentEnergy/startingEnergy;
        if(currentEnergy < varTrack.maxStam)
		    currentEnergy += amount * Time.deltaTime;

		if (currentEnergy <= 0) {

            // audManager.PlayOneShot(deathSound);
            currentEnergy = startingEnergy;
            energySlider.fillAmount = currentEnergy/startingEnergy;
            StartCoroutine(WaitForRecover());
        }
		
	}

    public void Win()
    {
        isDead = true;
        pScript.recovering = true;
    }

    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
       
        pScript.recovering = true;
        deathPanel.SetActive(true);
    } 
    
    

    IEnumerator WaitForFlash()
    {
        screenFlash.SetTrigger("Damage");
        yield return new WaitForSeconds(0.6f);
        damaged = false;
    }

    IEnumerator WaitForRecover()
    {
        pScript.recovering = true;
        screenFlash.SetTrigger("Recover");
        energyGain *= 4;
        yield return new WaitForSeconds(2);
        energyGain /= 4;
        pScript.recovering = false;
    }
}

