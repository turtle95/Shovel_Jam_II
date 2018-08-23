using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

   
    public Transform target; //this will be the thing the enemy is currently targeting

    //the tags are used to get a list of possible targets and compare their distance from the enemy
    public string croissantTag = "Croissant";
    public string enemyTag = "Enemy";
    public string playerTag = "Player";

   //toggles to control how the enemy behaves
    public bool chaseEnemy = false;
    public bool chaseCroissant = false;
    public bool croissantPriority = false;
    bool triggered = false;
    public bool trapDoor = false;
    bool awakened = false;

    public bool targetAquired = false; //bool for when enemy is currently locked onto something


    float counter = 0;  //keeps track of how long since the last calculation
    float multiplier = 1f; //adjusts the counter so that the enemies are not doing calculations at the same time

    GameObject[] tempArray; //used to store the target lists temporarily

    float closestDistSqr = Mathf.Infinity; //temporary variable for determining the closest target
    float distToTargetSqr = 0; //temporary variable for storing the distance between the player and current target beind looked at

    public float sightRange = 5f; //how far the enemies basic sight reaches
    Transform player; //transform specifically for the player so we don't have to find it again every time we need it

    public float triggerRange = 2f; //how close the player should be for the enemy to get pissed
    public float attackRange = 1.5f; //distance for the enemy to stop chasing and start fighting
    public bool attacking = false; //bool for when the enemy should be fighting, mainly used for other scripts

    //used for the attack and chase scripts to let them know not to blow up the precious croissants
    public bool croissantTargeted = false;

   

    EnemyHealth healthScript;
    public bool eating = false;

    EnemyAnimationController animScript;
   

    //initialize the player reference and mess with the multiplier
    private void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
        multiplier = Random.Range(0.7f, 1f);
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Transform>();
        sightRange *= sightRange;
        triggerRange *= triggerRange;
        attackRange *= attackRange;
    }




    void Update () {

        eating = healthScript.slowToEat;
        if (target == null)
        {
            targetAquired = false;
            attacking = false;
        }
        //when the counter is up, check for targets
        if (counter > 0.5f)
        {
            if (!eating)
            {
                //if a target has already been found then determine if you are in attacking range
                if (targetAquired)
                {
                    if (!awakened && trapDoor)
                    {
                        animScript.currentAnim = 7;
                        awakened = true;
                    }

                    distToTargetSqr = (target.position - transform.position).sqrMagnitude;
                    if (distToTargetSqr < attackRange)
                    {
                        if (croissantTargeted)
                            animScript.currentAnim = 6;
                        else
                            attacking = true;
                    }
                    else
                        attacking = false;
                } else
                {
                    if(awakened && trapDoor)
                    {
                        
                        animScript.currentAnim = 8;
                        awakened = false;
                    }
                }



                if (croissantPriority)
                {
                    if (target == null)
                    {
                        targetAquired = CheckForCroissant();
                        if (!targetAquired)
                            targetAquired = CheckForPlayer();
                        if (!targetAquired && chaseEnemy)
                            targetAquired = CheckForEnemy();
                    }
                    else if (!target.CompareTag(croissantTag))
                    {
                        targetAquired = CheckForCroissant();
                        if (!targetAquired)
                            targetAquired = CheckForPlayer();
                        if (!targetAquired && chaseEnemy)
                            targetAquired = CheckForEnemy();

                    }
                }
                else
                {
                    targetAquired = CheckForPlayer();
                    if (!targetAquired && chaseEnemy)
                        targetAquired = CheckForEnemy();
                    if (!targetAquired && chaseCroissant)
                        targetAquired = CheckForCroissant();
                }

            }
            counter = 0;
        }
        counter += Time.deltaTime * multiplier;
	}





    //each of these checks for a specific target type and sets the target to it if it is found, if it isn't found target is left as null and targetAquired is set to false
    bool CheckForPlayer()
    {
        
        croissantTargeted = false;
        closestDistSqr = Mathf.Infinity;
        target = player;
        distToTargetSqr = (target.position - transform.position).sqrMagnitude;

        

        if (distToTargetSqr < sightRange)
        {
            //enemy gets pissed if you get to close to it and makes you the main target regardless of any other objectives
            if (distToTargetSqr < triggerRange)
                triggered = true;

            return true;
        }
        else
        {
            
            target = null;
            return false;
        }
       
    }

    bool CheckForEnemy()
    {
        Debug.Log("Entered Loop E");
        croissantTargeted = false;
        target = null;
        closestDistSqr = Mathf.Infinity;
        tempArray = null;
        tempArray = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach(GameObject fart in tempArray)
        {
            distToTargetSqr = (fart.transform.position - transform.position).sqrMagnitude;
            if(distToTargetSqr < closestDistSqr)
            {
                if (fart.gameObject != this.gameObject)
                {
                    closestDistSqr = distToTargetSqr;
                    target = fart.transform;
                }
            }
        }
       
        if (target == null)
        {
            return false;
        }
        else return true;
    }

    bool CheckForCroissant()
    {
        Debug.Log("Entered Loop C");
        croissantTargeted = false;
        target = null;
        closestDistSqr = Mathf.Infinity;
        tempArray = null;
        tempArray = GameObject.FindGameObjectsWithTag(croissantTag);
        foreach (GameObject fart in tempArray)
        {
            distToTargetSqr = (fart.transform.position - transform.position).sqrMagnitude;
            if (distToTargetSqr < closestDistSqr)
            {
                closestDistSqr = distToTargetSqr;
                target = fart.transform;
            }
        }

        if (target == null)
        {
            return false;
        }
        else
        {
            croissantTargeted = true;
            return true;
        }
    }

}
