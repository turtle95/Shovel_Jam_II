using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Movement : MonoBehaviour {

	public Transform mover;
	public Transform planet;
	public float walkSpeed = 10;
	public float refWalkSpeed;
	public float gravity = -10;
	public float turnSpeed = 0.1f;
    public float runSpeed = 25f;

    //var for user movement input
    Vector3 movement;


	private Rigidbody rb;

    //var to calculate which direction is up
	Vector3 gravityUp;

	public Transform camDown;

	public float dashDistance = 15f;
	public ParticleSystem dashParticles;
	public ParticleSystem dashParticles2;


	public float distToGrounded = 1f;
	public float distToFall = 1.5f;


	public Transform cameraYOnly;
	public Transform cameraBox;

	public Stage3CamLook cScript;

    //The amount you fly upwards when launching yourself
	public float maxFlyHeight = 2.5f;


    //Soundses
    public AudioSource audManager;
    public AudioSource footstepManager;
    public AudioClip dashSound;
    public AudioClip walkingSound;
    public AudioClip jumpSound;


    //Jump stuffs
    int multiJump = 0;
    bool jumping = false;
    public float jumpForce = 30f;

    //bool for when you burst out of the planet
    public bool flying = false;

    //Ref to playerHealth for energy
    public PlayerHealth hScript;

    variableTracker varTrack;
    public bool recovering = false;

    bool sprintPressed = false;


    void Start () {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
        rb = GetComponent<Rigidbody> (); //assigns rb to the player's rigidbody
		refWalkSpeed = walkSpeed;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update ()
    {
        
		//rotates the player based on its relation to the planet, applies gravity
		WorldGravity();
       
		movement = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis("Vertical"));


		//rotates an empty game object so that it matches up with the camera only on the y axis
		cameraYOnly.localEulerAngles = new Vector3 (cameraYOnly.localEulerAngles.x, cameraBox.localEulerAngles.y, cameraYOnly.localEulerAngles.z);
		



		if (!(movement.x == 0 && movement.z == 0))
        {
            if (Grounded() == true && footstepManager.isPlaying == false)
            {
                if (Input.GetButtonDown("Sprint"))
                {
                    footstepManager.pitch = Random.Range(1.8f, 2.5f);
                }
                else
                {
                    footstepManager.pitch = Random.Range(.8f, 1.1f);
                }
                footstepManager.volume = Random.Range(.8f, 1f);
                footstepManager.Play();
            }

            //normalize the vector so we can use trigonometry
            Vector3 rotVec = new Vector3(movement.z, movement.x, 0).normalized;

			//just for the z rotation
			float rotDegree = (Mathf.Acos(rotVec.x/1)) * 180f/Mathf.PI;

			//if movement.x < 0 then multiply the degree angle by -1
			rotDegree *= (movement.x <0 ? -1 : 1);

			//rotate the degree around the y axis
			Quaternion rot = Quaternion.Euler(0,rotDegree,0);

			//rotations are done with multiply...this rotates the player model the right way!
			mover.rotation = Quaternion.Slerp(mover.rotation, cameraYOnly.rotation * rot, 0.5f);
		}


		//sets movement relative to camera
		movement = cameraYOnly.TransformDirection (movement);





        //dashes then calls the dash stop function
		if (Input.GetButtonDown ("Dash") && !recovering) {
            hScript.currentEnergy -= 10;
            audManager.PlayOneShot(dashSound);
			rb.AddForce(movement *dashDistance, ForceMode.Impulse);
            dashParticles.Play();
            StartCoroutine(DashStop());
		}



        //tells fixed update to jump if you aren't over your max jump amount
        if (Input.GetButtonDown("Jump") && multiJump < 3 && !recovering)
        {
            
            multiJump++;
            hScript.currentEnergy -= 5;
            audManager.PlayOneShot(jumpSound);
            jumping = true;

        }


        sprintPressed = Input.GetButton("Sprint");

        if (sprintPressed || Input.GetAxis("Left Trigger") != 0)
        {
            walkSpeed = runSpeed;
            hScript.energyGain = -2;

        }

        if (!sprintPressed && Input.GetAxis("Left Trigger") == 0)
        {
            hScript.energyGain = 1;
            walkSpeed = refWalkSpeed;
        }
        if (recovering)
            movement = Vector3.zero;
	}

    //Try to apply all the impulse physics stuff here
    private void FixedUpdate()
    {
        //if you are looking down at the right angle and you fire and you are not high enough up to be in a fast fall then shoot yourself upwards
        if (cScript.lookingDown && Input.GetButtonUp("Fire1") && NotFastFall())
        {
            rb.AddForce(transform.up * maxFlyHeight, ForceMode.VelocityChange);
        }


        if (jumping)
        {
            
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            jumping = false;
        }

        if (flying)
        {
            rb.AddForce(transform.up * jumpForce * 3f, ForceMode.VelocityChange);
            flying = false;
        }

        //moves the player without directly adjusting its velocity, allows unity's gravity to keep working
        rb.MovePosition(rb.position + movement * walkSpeed * Time.deltaTime);

    }


    


    //true when you are too close to use fast fall... no fast fall is implemented right now, really just used to make shooting yourself upwards funner
    bool NotFastFall(){
		return Physics.Raycast (transform.position,-transform.up, distToFall);
	}

	//true when you are on the ground
	bool Grounded()
	{
		return Physics.Raycast (transform.position, -transform.up, distToGrounded);
	}




    //Makes dash stop suddenly rather than leaving you with momentum to fight against
    IEnumerator DashStop()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector3.zero;
    }




    public void WorldGravity()
    {
        //gets the distance between player and planet, essentially this is the direction you want to be facing

        if (varTrack.outsidePlanet)
        {
            gravityUp = (transform.position - planet.position).normalized;
            //walkSpeed += Time.deltaTime * 2;
            //runSpeed += Time.deltaTime * 2;
        }
        else
            gravityUp = (planet.position - transform.position).normalized;
       
        //the up direction for the player
        Vector3 turdsUp = transform.up;

        //applies gravity based on the player's local down
        //if you're grounded then don't apply gravity and reset your jump count
        if (Grounded())  
            multiJump = 0;
        else
            rb.AddForce(turdsUp * gravity);
        

		//creates a rotation between the gravityUp and the player's current up
		Quaternion targetRotation = Quaternion.FromToRotation(turdsUp, gravityUp) * transform.rotation;
	
		//smoothly rotates the player to the target rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 50 * Time.deltaTime);
	}
}
