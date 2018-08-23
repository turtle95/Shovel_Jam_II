using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {

    //If the enemy doesn't move then leave out both this script and the EnemyWander script

    //the speeds the enemy will be running and turning at
    public float runSpeed = 10;
    public float runTurnSpeed = 8;

    //whether the enemy flies or runs
    public bool flying = true;
    public bool noMove = false;

    //reference to sight script for target and whatnot
    EnemySight sightScript;

    //temp variables for movement
    Quaternion neededRot;
    Vector3 goalPos = Vector3.zero;


    //stuff for flying gravity rotationy things
    Vector3 gravityUp = Vector3.zero;
    public Transform planet;
    public Transform model;
    Quaternion targetRotation;

    //stuff for slowing down to eat
    float tempRunSpeed =0;
    float tempTurnSpeed = 0;

    private void Start()
    {
        tempRunSpeed = runSpeed;
        tempTurnSpeed = runTurnSpeed;
        sightScript = GetComponent<EnemySight>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        if(!flying)
            InvokeRepeating("GroundGravity", Random.Range(0f, 3f), 0.02f); //Reduce last number to smooth slope transitions
    }

    //if yur not attacking and there is a target then chase it
    void Update () {

        if (sightScript.eating)
        {
            runSpeed = 0;
            runTurnSpeed = 0;
        }
        else
        {
            runSpeed = tempRunSpeed;
            runTurnSpeed = tempTurnSpeed;
        }

        if (!noMove)
        {
            if (!sightScript.attacking && sightScript.targetAquired)
            {

                if (flying)
                    ChaseFlying();
                else
                    ChaseGround();
            }
        }
        else
        {
            if (sightScript.targetAquired)
                Potato();
        }
	}

    //sits and looks at target
    void Potato()
    {
        goalPos = sightScript.target.position;
        neededRot = Quaternion.LookRotation(goalPos - transform.position);
        Vector3 uno = targetRotation.eulerAngles;
        neededRot = Quaternion.Euler(neededRot.eulerAngles.x, neededRot.eulerAngles.y, uno.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, neededRot, Time.deltaTime * runTurnSpeed);
    }


    void ChaseFlying()
    {
        FlyingGravity();

        goalPos = sightScript.target.position;

       


        //calculates the wanted rotation based on the current destination waypoint then roatates
        neededRot = Quaternion.LookRotation(goalPos - transform.position);
        Vector3 uno = targetRotation.eulerAngles;
        neededRot = Quaternion.Euler(neededRot.eulerAngles.x, neededRot.eulerAngles.y, uno.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, neededRot, Time.deltaTime * runTurnSpeed);

        //moves in forward direction based on the speed
        transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
    }

    void ChaseGround()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(sightScript.target.position - transform.position, transform.up), runTurnSpeed * Time.deltaTime); //turn TOWARD the player
        transform.position += transform.forward * runSpeed * Time.deltaTime; //move forward

    }

    void GroundGravity()
    {
        //------------------------- Rotate to match terrain while continuing to face player -------------------------------
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + (transform.up * 1), -transform.up, out _hit)) //add a terrain layer mask here to only stick to terrain
        {
            transform.position = _hit.point + transform.up * 0.1f; //stick to terrain

            Vector3 _proj = transform.forward - (Vector3.Dot(transform.forward, _hit.normal)) * _hit.normal; //match normal
            transform.rotation = Quaternion.LookRotation(_proj, _hit.normal);
        }
    }

    void FlyingGravity()
    {
        //gets the distance between enemy and planet, essentially this is the direction you want to be facing
        gravityUp = (planet.position - transform.position).normalized;

        //the up direction for the enemy
        Vector3 turdsUp = transform.up;

       
        //creates a rotation between the gravityUp and the enemy's current up
       targetRotation = Quaternion.FromToRotation(turdsUp, gravityUp) * transform.rotation;

        //smoothly rotates the enemy to the target rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }

}
