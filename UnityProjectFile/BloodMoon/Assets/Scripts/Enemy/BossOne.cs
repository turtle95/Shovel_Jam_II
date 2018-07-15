using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour {

    public float gravity = -10;
    public Transform planet;
    public Transform player;
    public int speed = 10;
    public Transform eyes;
    public Rigidbody rb;
    public float walkSpeed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WorldGravity();
        //transform.Translate(eyes.forward * Time.deltaTime * speed);

        //moves the player without directly adjusting its velocity, allows unity's gravity to keep working
        rb.MovePosition(rb.position + Vector3.forward * walkSpeed * Time.deltaTime);

    }

    public void WorldGravity()
    {
        //gets the distance between player and planet, essentially this is the direction you want to be facing
        //gravityUp = (transform.position - planet.position).normalized;
        Vector3 gravityUp = (planet.position - transform.position).normalized;
        //the up direction for the player
        Vector3 turdsUp = transform.up;

        //applies gravity based on the player's local down
        rb.AddForce(turdsUp * gravity);

        //creates a rotation between the gravityUp and the player's current up
        Quaternion targetRotation = Quaternion.FromToRotation(turdsUp, gravityUp) * transform.rotation;
        //Vector3 = new Vector3(transform.position.x, transform.position);
        
        // targetRotation = Quaternion.Euler(new Vector3(targetRotation.x, eyes.rotation.y, targetRotation.z));

        Vector3 newPayPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        eyes.LookAt(newPayPos);
      //  eyes.Roatat
        //targetRotation = Quaternion.Euler(new Vector3(targetRotation.x, eyes.rotation.y, targetRotation.z));
        //smoothly rotates the player to the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
        //transform.LookAt(newPayPos);
        //transform.localEulerAngles = (new Vector3(transform.localEulerAngles.x, eyes.localEulerAngles.y, transform.localEulerAngles.z));
    }
}

