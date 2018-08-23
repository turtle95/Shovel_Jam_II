using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour {

    EnemySight sightScript;

    public float speed = 5;
    public float turnSpeed = 2;
    private Quaternion neededRot;

    public bool flying = true;

    //toggles between waypoint movement and random area based movement
    public bool useWaypoints = false;

    //toggles whether enemy will follow a set path or randomly choose waypoints
    //only matters when waypoints are being used
    public bool randomWaypoints = true;

    //waypoint crap
    GameObject[] wayPoints;
    public string wayPointTag = "flyingWaypoints";
    int destination = 0;
    int prevDest = 0;
    int oldAssDest = 0;



    //boundry movement crap
    public Vector3 boundry = Vector3.zero;
    Vector3 goalPos = Vector3.zero;

    //rate at which a new destination within the boundry is set
    public int turnRate = 10000;

    //a transform that will be used to determine the center of the boundry
    public Transform boundryCenter;

    //stuff for flying gravity rotationy things
    Vector3 gravityUp = Vector3.zero;
    public Transform planet;



    //for the ground based wandering
    bool wandering = false;

    //stuff for slowing down to eat
    float tempRunSpeed = 0;
    float tempTurnSpeed = 0;


    // Use this for initialization
    void Start () {
        tempRunSpeed = speed;
        tempTurnSpeed = turnSpeed;
        sightScript = GetComponent<EnemySight>();
        if (flying)
        {
            if (useWaypoints)
            {
                wayPoints = GameObject.FindGameObjectsWithTag(wayPointTag);
                goalPos = wayPoints[0].GetComponent<Transform>().position;
            }

            planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (sightScript.eating)
        {
            speed = 0;
            turnSpeed = 0;
        }
        else
        {
            speed = tempRunSpeed;
            turnSpeed = tempTurnSpeed;
        }


        if (!sightScript.targetAquired)
        {
            if (flying)
            {
                if (!useWaypoints)
                    BoundryMovement();

                MainMovement();
            }
            else if(!wandering)
                StartCoroutine(GroundWander());
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(boundryCenter.position, boundry);
        Gizmos.color = Color.blue;
    }

    void BoundryMovement()
    {
        if(Random.Range(1, turnRate) < 50)
        {
            goalPos = new Vector3((boundryCenter.position.x + Random.Range(-boundry.x, boundry.x)),
               (boundryCenter.position.y + Random.Range(-boundry.y, boundry.y)),
               (boundryCenter.position.z + Random.Range(-boundry.z, boundry.z)));
        }
    }


    //moves based on waypoints
    void MainMovement()
    {
        FlyingGravity();

        //moves in forward direction based on the speed
        transform.Translate(Vector3.forward * Time.deltaTime * speed);


        //calculated the wanted rotation based on the current destination waypoint then roatates
        neededRot = Quaternion.LookRotation(goalPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, neededRot, Time.deltaTime * turnSpeed);
        
    }




    void FlyingGravity()
    {
        //gets the distance between enemy and planet, essentially this is the direction you want to be facing
        gravityUp = (planet.position - transform.position).normalized;

        //the up direction for the enemy
        Vector3 turdsUp = transform.up;


        //creates a rotation between the gravityUp and the enemy's current up
        Quaternion targetRotation = Quaternion.FromToRotation(turdsUp, gravityUp) * transform.rotation;

        //smoothly rotates the enemy to the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
    }




    //meant for waypoints, when enemy reaches one, it will choose a new destination
    private void OnTriggerEnter(Collider other)
    {
        if (useWaypoints)
        {
           
            if (other.CompareTag(wayPointTag))
            {
                //if waypoints are set to random, it will choose a new waypoint until it finds one that isn't the previous destination or the one before that
                if (randomWaypoints)
                {
                    oldAssDest = prevDest;
                    prevDest = destination;

                    do
                    {
                        destination = Random.Range(0, wayPoints.Length);
                    } while (destination == prevDest || destination == oldAssDest);
                   
                }
                else //if waypoints aren't set to random, it will just continue to the next waypoint
                {
                    destination++;
                    if (destination > (wayPoints.Length-1))
                        destination = 0;
                }
                goalPos = wayPoints[destination].GetComponent<Transform>().position;
            }
        }
    }

    //copied from the spider script, has ground movement wandering
    private IEnumerator GroundWander()
    {
        wandering = true;

        float timePassed = 0f;
        float timeToPerform = Random.Range(0.5f, 5.5f);
        while (timePassed < timeToPerform) //walk straight
        {
            transform.position += transform.forward * (speed / 2) * Time.deltaTime; //move forward
            timePassed += Time.deltaTime;
            yield return null;
        }

        Vector3 direction = transform.right;
        if (Random.Range(0, 1) < 0.6f)
        {
            direction = -transform.right;
        }

        timePassed = 0f;
        timeToPerform = Random.Range(0.5f, 5.5f);
        while (timePassed < 2f) //walk turn
        {
            transform.Rotate(direction * Random.Range(15f, 70f) * Time.deltaTime);
            transform.position += transform.forward * (speed / 2) * Time.deltaTime; //move forward
            timePassed += Time.deltaTime;
            yield return null;
        }

        wandering = false;
    }
}
