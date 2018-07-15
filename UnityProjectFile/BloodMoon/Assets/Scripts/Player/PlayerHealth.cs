using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	// Health
    public float startingHealth = 100;                            // The amount of health the player starts the game with.
    public float currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    // public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

	// Energy
	public float startingEnergy = 1000;
	public float currentEnergy;
	public Slider energySlider;

    public AudioSource audManager;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip nightJingle;

    public DayNightCycle sun;
	
    // Animator anim;                                              // Reference to the Animator component.
    // AudioSource playerAudio;                                    // Reference to the AudioSource component.
    // PlayerMovement playerMovement;                              // Reference to the player's movement.
    // PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.

	bool isNight;												// Whether it is nighttime.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake ()
    {
        // Setting up the references.
        // anim = GetComponent  ();
        // playerAudio = GetComponent  ();
        // playerMovement = GetComponent  ();
        // playerShooting = GetComponentInChildren  ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
		currentEnergy = startingEnergy;

		healthSlider.value = currentHealth;
		energySlider.value = currentEnergy;
    }


    void Update ()
    {
        LoseEnergy(1);
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            // damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            // damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
		
    }

    public void TakeDamage (int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        audManager.PlayOneShot(hurtSound);

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            audManager.PlayOneShot(deathSound);
            Death ();
        }
    }

	public void LoseEnergy (int amount) 
	{
		currentEnergy -= amount * Time.deltaTime;
		energySlider.value = currentEnergy;

		if (currentEnergy <= 0 && sun.isNight)  {
            // TODO have player collapse and spiders surround him.
            audManager.PlayOneShot(deathSound);
            Death();
		}
		else if (currentEnergy <= 0 && !sun.isNight) {
            // TODO have player collapse

            // TODO wait a few seconds for animation
            audManager.PlayOneShot(nightJingle);
            currentEnergy = startingEnergy;
            energySlider.value = currentEnergy;

            sun.ChangeToNight();
		}
	}


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects ();

        // Tell the animator that the player is dead.
        // anim.SetTrigger ("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        // playerAudio.clip = deathClip;
        // playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        // playerMovement.enabled = false;
        // playerShooting.enabled = false;
    }       
}

