using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //references to health and sight scripts,needed for exploding enemies and to determine when to attack
    EnemySight sightScript;
    EnemyHealth healthScript;
    EnemyChase chaseScript;
    

    //switches for whether it is a ranged, melee, or explosion/spray emitting enemy
    public bool ranged = false;
    bool stinkSpray = false;
    bool exploding = false;

    
   //shooting stuffs
    public GameObject bullet;  //the prefab to be spawned when shooting, exploading, or spraying
    Rigidbody rb;  //reference to the bullet's prefab, allows us to add force to it and let it have it's own physics
    public float shootForce = 5; //the force that a bullet is fired off at(it's speed)
    public Transform aimer; //the place(and direction) that a bullet will be fired off at - need code to make it actually aim at stuff
    public float fireRate = 2; //the rate in seconds that bullet will be fired at



    bool fighting = false; //switch to help keep from firing off more than one instance of a coroutine, also makes script more efficient
    public bool charge = false; //switch for whether the enemy charges at the target
    public bool lunge = false; //switch for whether the enemy lunges/jumps at the target(essentially gives walking enemies flying movement during the lunge then switches them back)


   
    


    private void Start()
    {
        sightScript = GetComponent<EnemySight>();
        healthScript = GetComponent<EnemyHealth>();
        chaseScript = GetComponent<EnemyChase>();
    }

    void Update () {
        if (ranged)
            Ranged();
        else
            Melee();
	}

    //just decides when to call the coroutine
    void Ranged()
    {
        if (sightScript.attacking && !fighting)
            StartCoroutine(ShootStuff());
    }


    //shoots, sprays, or exploads, then if the target is still in range, it will call itself again, otherwise ranged has to call it
    IEnumerator ShootStuff()
    {
        fighting = true;
        yield return new WaitForSeconds(fireRate);
        if (sightScript.attacking)
        {
            aimer.LookAt(sightScript.target.position);
            if (!stinkSpray)
            {
              rb = Instantiate(bullet, aimer.position, aimer.rotation).GetComponent<Rigidbody>();
             rb.AddForce(aimer.forward * shootForce, ForceMode.Impulse);
            }
            else
            {
                Instantiate(bullet, aimer.position, aimer.rotation);
                if (exploding)
                    healthScript.health = 0; //if exploding then dies after explosion
            }
        
            StartCoroutine(ShootStuff());
        }
        else
            fighting = false;
    }



    //if normal melee, just plays the animation until target is out of range, if charge ups the chase speed, if lunge jumps at the target
    void Melee()
    {

        if (sightScript.attacking && !fighting)
        {
            if (charge || lunge)
                StartCoroutine(ChargeAndLunge());

            fighting = true;
        }
        else
        {
            if (charge)
                chaseScript.runSpeed /= 4;
            if (lunge)
            {
                chaseScript.runSpeed /= 8;
                chaseScript.flying = false;
            }

            fighting = false;
           // mainAnim.SetTrigger(idleTrigger);
        }
    }

    IEnumerator ChargeAndLunge()
    {
        yield return new WaitForSeconds(1);
        if (charge)
        {
            chaseScript.runSpeed *= 4;
            // mainAnim.SetTrigger(chargeTrigger);
        }
        else if (lunge)
        {
            chaseScript.runSpeed *= 8;
            chaseScript.flying = true;
            // mainAnim.SetTrigger(lungeTrigger);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!ranged && collision.gameObject.CompareTag("Enemy") && sightScript.attacking)
        {
            Destroy(collision.gameObject);
        }
    }
}
