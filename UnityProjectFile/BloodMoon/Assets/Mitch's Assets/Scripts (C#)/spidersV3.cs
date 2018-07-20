using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spidersV3 : MonoBehaviour
{
    public LayerMask terrainMask;
    public int health = 5;
    public int moveSpeed = 20;
    public int rotationSpeed = 5;
    public GameObject pickup;
    [HideInInspector] public bool runAway = false;
    public Transform planetTransform;
    [HideInInspector] public Vector3 gravityUp;


    private Transform target; //transform of the player
    private bool aggro; //is the player in range of the mob?

    public Transform planet;
    public float gravity = -10;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindWithTag("Player").transform;

        ////Stick on terrain
        //RaycastHit _hit;
        //if (Physics.Raycast(transform.position, -Vector3.up, out _hit)) //send a raycst down
        //{
        //    float _distanceToGround = _hit.distance; //distance raycast traveled to reach ground
        //    float heightToAdd = transform.localScale.y; //height of the object (otherwise the top of our object would be aligned with the terrain e.i. underneath)
        //    transform.position = new Vector3(transform.position.x, transform.position.y - _distanceToGround + (heightToAdd / 10), transform.position.z); //subtract the distanceToGround from our current y position. Now add our objects height to place it on top of the terrain
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePlayerTransform((transform.position - target.transform.position).normalized);

        //if (aggro == true)
        //{
        if (runAway == true)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - target.position), rotationSpeed * Time.deltaTime); //turn AWAY from the player
        }
        else
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime); //turn TOWARD the player
        }
        //transform.position += transform.forward * moveSpeed * Time.deltaTime; //move forward

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, transform.up), rotationSpeed * Time.deltaTime); //turn TOWARD the player
                                                                                                                                                                                //transform.LookAt(target.position);
        Vector3 _forward = transform.forward;
        transform.position += _forward * moveSpeed * Time.deltaTime;

        #region Old Stuff
        /*
        //Debug.DrawRay(transform.position, -transform.up, Color.green);
        //Stick on terrain (!!! Coud replace this with a rigidbody setup !!!)
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + (transform.up * 1 + transform.forward * 1), -transform.up, out _hit, terrainMask))
        {
            //Vector3 _forward = transform.forward;
            //transform.position += (_forward + _hit.point.normalized) * moveSpeed * Time.deltaTime;

            float normalForward = _hit.normal.z;
            //Debug.Log("Hit");
            //if (_hit.distance > 1)
            //{
            //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (_hit.distance - 1), transform.localPosition.z);
            //}
            //else if (_hit.distance < 1)
            //{
            //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (1 - _hit.distance), transform.localPosition.z);
            //}
            //transform.position = _hit.point + _hit.transform.forward * moveSpeed * Time.deltaTime;

            //Vector3 _proj = _forward - (Vector3.Dot(_forward, _hit.normal)) * _hit.normal;
            //transform.rotation = Quaternion.LookRotation(_proj, _hit.normal);

            //Vector3 _relativePos = target.position - transform.position;
            //Quaternion _desiredAngle = Quaternion.LookRotation(_relativePos);
            //Vector3 _eulerAngle = _desiredAngle.eulerAngles;
            //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, _eulerAngle.y, transform.rotation.z));

            //Vector3 desiredAngle = transform.LookAt(target.position);

            //float _heightDifference = transform.position.y - target.transform.position.y;

            //Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            //transform.transform.LookAt(targetPosition);

            //transform.position = _hit.point + _hit.normal * 0.1f; // move to floor and apply small upward offset along normal
            //transform.rotation

            //float distancetoground = _hit.distance; //distance raycast traveled to reach ground
            //var heightToAdd = transform.localScale.y; //height of object (otherwise the top of our object would be aligned with the terrain e.i. underneath)
            //transform.position = new Vector3(transform.position.x, (transform.position.y - distancetoground + (heightToAdd / 10)), transform.position.z); //subtract the distancetoground from out current y position. Now add our objects height to place it on top of the terrain

            //transform.rotation = Quaternion.Euler(new Vector3(_hit.transform.rotation.x, transform.rotation.y, _hit.transform.rotation.z));

            //Vector3 desiredPosition =  Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            //transform.position = new Vector3(desiredPosition.x, _hit.point.y, desiredPosition.z);
            //Quaternion desiredRotation = Quaternion.FromToRotation(transform.up, _hit.normal); //rotate to match normal
            //transform.rotation = Quaternion.Euler(new Vector3(desiredRotation.x, (target.position - transform.position).y, desiredRotation.z));

            //Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            //transform.transform.LookAt(targetPosition);
            //}

            //transform.position += transform.forward * moveSpeed * Time.deltaTime; //move forward
            }
        */
        #endregion

        WorldGravity();
    }

    private void LateUpdate() //late updated does not interfere with the movement calculations happening in update
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position + (transform.up * 1 + transform.forward * 1), -transform.up, out _hit, terrainMask))
        {
            Vector3 _proj = transform.forward - (Vector3.Dot(transform.forward, _hit.normal)) * _hit.normal;
            transform.rotation = Quaternion.LookRotation(_proj, _hit.normal);
        }
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

    //private void UpdatePlayerTransform(Vector3 movementDirection)
    //{
    //    RaycastHit hitInfo;

    //    if (GetRaycastDownAtNewPosition(movementDirection, out hitInfo))
    //    {
    //        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    //        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, float.PositiveInfinity);

    //        transform.rotation = finalRotation;
    //        transform.position = hitInfo.point + hitInfo.normal * .5f;
    //    }
    //}

    //private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit hitInfo)
    //{
    //    Vector3 newPosition = transform.position;
    //    Ray ray = new Ray(transform.position + movementDirection * moveSpeed, -transform.up);

    //    if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, terrainMask))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    public void WorldGravity()
    {
        //gets the distance between player and planet, essentially this is the direction you want to be facing
        //gravityUp = (transform.position - planet.position).normalized;
        gravityUp = (planet.position - transform.position).normalized;
        //the up direction for the player
        Vector3 turdsUp = transform.up;

        //applies gravity based on the player's local down
        rb.AddForce(turdsUp * gravity);

        //creates a rotation between the gravityUp and the player's current up
        Quaternion targetRotation = Quaternion.FromToRotation(turdsUp, gravityUp) * transform.rotation;

        //smoothly rotates the player to the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }
}

