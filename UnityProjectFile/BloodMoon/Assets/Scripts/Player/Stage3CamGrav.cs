using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3CamGrav : MonoBehaviour {

	public float gravity = -10;
	public Transform downLooker;
	GameObject planet;

    public bool outsidePlanet = false;

	Quaternion localRotPlayer; //quaternion to assign the player's rotation
	
	public int invert = 1; //value to toggle inverse/normal camera movement
	public GameObject player;

	public float smoothSpeed = 0.125f;

	public Transform cam;
	private Vector3 velocity = Vector3.one;
	Transform playerCenter;
	public Vector3 offSet;
    Vector3 gravityUp;
   

    

    void Start(){
		planet = GameObject.FindGameObjectWithTag ("Planet");
       
        
        if (outsidePlanet)
            gravityUp = (transform.position - planet.transform.position).normalized;
        else
            gravityUp = (planet.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation (transform.up, gravityUp) * transform.rotation;
		playerCenter = player.GetComponent<Transform>();
		
	}

	void Update(){
        
        
        if (outsidePlanet)
		    gravityUp = (transform.position - planet.transform.position).normalized;
        else
           gravityUp = (planet.transform.position - transform.position).normalized;


        Vector3 turdsUp = downLooker.up;
		Quaternion targetRotation = Quaternion.FromToRotation (turdsUp, gravityUp) * downLooker.rotation;
		downLooker.rotation = Quaternion.Slerp (downLooker.rotation, targetRotation, 50 * Time.deltaTime);
		transform.rotation = Quaternion.FromToRotation (transform.up, gravityUp) * transform.rotation;
	}





	// Update is called once per frame
	void FixedUpdate () {
		FollowPlayer ();
	}



	void FollowPlayer()
	{
		Vector3 desiredPos = playerCenter.position;

		Vector3 smoothedPos = Vector3.SmoothDamp (transform.position, desiredPos, ref velocity, smoothSpeed);

		transform.position = smoothedPos;

	}

}
