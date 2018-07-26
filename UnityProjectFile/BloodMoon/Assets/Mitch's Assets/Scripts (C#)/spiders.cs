using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiders : MonoBehaviour
{
    public GameObject graphics;
    public float health = 5;
    public int moveSpeed = 20;
    public int rotationSpeed = 5;
    public GameObject pickup;
    [HideInInspector] public bool runAway = false;
    [HideInInspector] public Vector3 gravityUp;

    [HideInInspector] public Transform target; //transform of the player
    public bool aggro; //is the player in range of the mob?

    public GameObject burnEffect;
    public bool burning = false;

    [HideInInspector] public Transform planet;
    public float gravity = -10;
    private Rigidbody rb;
    private Animation _animation;
    private bool _wandering = false;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogWarning("Couldn't find the 'Player' tag. Deleting Spider.");
            Destroy(gameObject);
        }
        _animation = GetComponent<Animation>();
        //planet = FindClosestTag("Planet").transform; //use this to remove any connections to the spiderSpawner script
        //if (planet == null) Debug.LogError("No active objects have the 'Planet' tag");
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

        if (aggro == true) //attack
        {
            _wandering = false;
            if (runAway == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - target.position, transform.up), rotationSpeed * Time.deltaTime); //turn AWAY from the player
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, transform.up), rotationSpeed * Time.deltaTime); //turn TOWARD the player
            }
            transform.position += transform.forward * moveSpeed * Time.deltaTime; //move forward

            //Vector3 _forward = transform.forward;
            //transform.position += _forward * moveSpeed * Time.deltaTime;
        }
        else //wander
        {
            if (_wandering == false) StartCoroutine(Wander());
        }

        WorldGravity();
    }

    private void LateUpdate()
    {
        burning = false;

        if (aggro == true)
        {
            //------------------------- Rotate to match terrain while continuing to face player -------------------------------
            RaycastHit _hit;
            if (Physics.Raycast(transform.position + (transform.up * 1 + transform.forward * 1), -transform.up, out _hit))
            {
                //Vector3 _proj = transform.forward - (Vector3.Dot(transform.forward, _hit.normal)) * _hit.normal;
                //transform.rotation = Quaternion.LookRotation(_proj, _hit.normal);
            }
        }
    }

    private IEnumerator Wander()
    {
        //Debug.Log("Wandering");
        _wandering = true;
        _animation.Play("run");
        _animation["run"].speed = 2;

        float timePassed = 0f;
        float timeToPerform = Random.Range(0.5f, 5.5f);
        while (timePassed < timeToPerform) //walk straight
        {
            transform.position += transform.forward * (moveSpeed / 2) * Time.deltaTime; //move forward
            timePassed += Time.deltaTime;
            yield return null;
        }

        Vector3 direction = transform.right;
        if (Random.Range(0, 1) == 0)
        {
            direction = -transform.right;
        }

        timePassed = 0f;
        timeToPerform = Random.Range(0.5f, 5.5f);
        while (timePassed < 2f) //walk turn
        {
            transform.Rotate(direction * Random.Range(15f, 70f) * Time.deltaTime);
            transform.position += transform.forward * (moveSpeed / 2) * Time.deltaTime; //move forward
            timePassed += Time.deltaTime;
            yield return null;
        }

        _wandering = false;
    }

    public void arrowHit(int arrowDamage)
    {
        health -= arrowDamage;
        //TO-DO: moveSpeed = moveSpeed * 0.4; //reduce speed after being hit
        if (health <= 0)
        {
            Instantiate(pickup, transform.position, transform.rotation);
            _animation.Play("death1");
            aggro = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
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
            _animation.Play("death");
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
            _animation.Play("run");
            _animation["run"].speed = 4;
        }
        if (otherObj.tag == "Boundary")
        { //if the mob runs into the ocean (boundary) destroy it
            Destroy(gameObject);
        }
    }

    private GameObject FindClosestTag(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

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
