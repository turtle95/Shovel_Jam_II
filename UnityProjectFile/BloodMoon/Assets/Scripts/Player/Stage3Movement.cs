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
	//public CameraController camScript;

	Vector3 movement;
	private Rigidbody rb;

	Vector3 gravityUp;
	Collider enemCol;
	bool triggered = false;

	//float ySensitivity = 0.9f;

	public Transform camDown;

	public float dashDistance = 15f;
	public ParticleSystem dashParticles;
	public ParticleSystem dashParticles2;


	public float distToGrounded = 1f;
	public float distToFall = 5f;


	public Transform cameraYOnly;
	public Transform cameraBox;

	public Stage3CamLook cScript;
	public float maxFlyHeight = 2.5f;

    public AudioSource audManager;
    public AudioSource footstepManager;
    public AudioClip dashSound;
    public AudioClip walkingSound;
    public AudioClip jumpSound;

	public GameObject infectedFog;

    //Jump stuffs
    int multiJump = 0;
    bool jumping = false;

    public float jumpForce = 30f;

    public float runSpeed = 25f;

    //Ref to playerHealth for energy
    public PlayerHealth hScript;


    public bool outsidePlanet = false;

	void Start () {
        
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
                if (Input.GetButtonDown("Fire3"))
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
		//movement = Camera.main.transform.TransformDirection(movement);
		movement = cameraYOnly.TransformDirection (movement);

		if (triggered && !enemCol) {
			walkSpeed = refWalkSpeed;
			infectedFog.SetActive (false);
			triggered = false;
		}


		if (Input.GetButtonDown ("Fire2")) {
            hScript.currentEnergy -= 10;
            audManager.PlayOneShot(dashSound);
			rb.AddForce(movement *dashDistance, ForceMode.Impulse);
            dashParticles.Play();
            StartCoroutine(DashStop());
			
		//	dashParticles2.Play ();
		}

        if (Input.GetButtonDown("Jump") && multiJump < 3)
        {
            
            multiJump++;
            //Debug.Log(multiJump);
            hScript.currentEnergy -= 5;
            audManager.PlayOneShot(jumpSound);
            jumping = true;
            
            
          //  dashParticles.Play();
          //  dashParticles2.Play();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            walkSpeed = runSpeed;
            hScript.energyGain = -2;
           // dashParticles.Play();
           // dashParticles2.Play();
        }

        if (Input.GetButtonUp("Fire3"))
        {
            hScript.energyGain = 1;
            walkSpeed = refWalkSpeed;
        }



      
	}


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

        //moves the player without directly adjusting its velocity, allows unity's gravity to keep working
        rb.MovePosition(rb.position + movement * walkSpeed * Time.deltaTime);

    }

    //true when you are too close to use fast fall
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
        //Vector3 orientedZero = rb.velocity;
        //orientedZero = cameraYOnly.TransformDirection(orientedZero);
        //orientedZero = new Vector3(0, orientedZero.y, 0);
        rb.velocity = Vector3.zero;
    }


	//void OnTriggerStay(Collider col){
	//	if (col.CompareTag ("Fog")) {
	//		infectedFog.SetActive (true);
	//		walkSpeed = 3;
	//		enemCol = col;
	//		triggered = true;
	//	}
	//}

	//void OnTriggerExit(Collider col){
	//	if (col.CompareTag ("Fog")) {
	//		infectedFog.SetActive (false);
	//		walkSpeed = refWalkSpeed;
	//		triggered = false;
	//	}
	//}


	public void WorldGravity()
	{
		//gets the distance between player and planet, essentially this is the direction you want to be facing

        if(outsidePlanet)
		    gravityUp = (transform.position - planet.position).normalized;
        else
            gravityUp = (planet.position - transform.position ).normalized;
        //the up direction for the player
        Vector3 turdsUp = transform.up;

        //applies gravity based on the player's local down
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
