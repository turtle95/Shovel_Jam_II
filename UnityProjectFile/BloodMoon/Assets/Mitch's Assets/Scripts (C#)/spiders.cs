using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiders : MonoBehaviour
{

    public float health = 5;
    public int moveSpeed = 20;
    public int rotationSpeed = 5;
    public GameObject pickup;
    [HideInInspector] public bool runAway = false;

    private Transform target; //transform of the player
    private bool aggro; //is the player in range of the mob?

    public GameObject burnEffect;
    public bool burning = false;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        //Stick on terrain
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out _hit)) //send a raycst down
        {
            float _distanceToGround = _hit.distance; //distance raycast traveled to reach ground
            float heightToAdd = transform.localScale.y; //height of the object (otherwise the top of our object would be aligned with the terrain e.i. underneath)
            transform.position = new Vector3(transform.position.x, transform.position.y - _distanceToGround + (heightToAdd / 8), transform.position.z); //subtract the distanceToGround from our current y position. Now add our objects height to place it on top of the terrain
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (burning)
        {
            sprayHit();
        }
        else
        {
            burnEffect.SetActive(false);
        }

        if (aggro == true)
        {
            if (runAway == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - target.position), rotationSpeed * Time.deltaTime); //turn AWAY from the player
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime); //turn TOWARD the player
            }
            transform.position += transform.forward * moveSpeed * Time.deltaTime; //move forward

            //Stick on terrain (!!! Coud replace this with a rigidbody setup !!!)
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out _hit))
            {
                float distancetoground = _hit.distance; //distance raycast traveled to reach ground
                var heightToAdd = transform.localScale.y; //height of object (otherwise the top of our object would be aligned with the terrain e.i. underneath)
                transform.position = new Vector3(transform.position.x, (transform.position.y - distancetoground + (heightToAdd / 10)), transform.position.z); //subtract the distancetoground from out current y position. Now add our objects height to place it on top of the terrain
            }
        }
    }

    private void LateUpdate()
    {
        burning = false;
    }

    public void arrowHit(int arrowDamage)
    {
        health -= arrowDamage;
        //TO-DO: moveSpeed = moveSpeed * 0.4; //reduce speed after being hit
        if (health <= 0)
        {
            Instantiate(pickup, transform.position, transform.rotation);
            GetComponent<Animation>().Play("death");
            aggro = false;
            Destroy(gameObject, 1.5f);
        }
        aggro = true; //mob was hit and is now aggroed
    }

    //take damage when hit with spray
    public void sprayHit()
    {
        burnEffect.SetActive(true);
        health -= 5f * Time.deltaTime;
        
        //TO-DO: moveSpeed = moveSpeed * 0.4; //reduce speed after being hit
        if (health <= 0)
        {
            Instantiate(pickup, transform.position, transform.rotation);
            GetComponent<Animation>().Play("death");
            aggro = false;
            Destroy(gameObject, 1.5f);
        }
        aggro = true; //mob was hit and is now aggroed
    }

    private void OnTriggerEnter(Collider otherObj)
    { //When the parent has a rigidbody attached it will become a compound collider recieve trigger events from it's children (use a Kinematic rigidbody to act like a simple collider)
        if (otherObj.tag == "Player")
        { //when the player is in range, turn on movement
            aggro = true;
            GetComponent<Animation>().Play("run");
            GetComponent<Animation>()["run"].speed = 2;
        }
        if (otherObj.tag == "Boundary")
        { //if the mob runs into the ocean (boundary) destroy it
            Destroy(gameObject);
        }
    }
}
