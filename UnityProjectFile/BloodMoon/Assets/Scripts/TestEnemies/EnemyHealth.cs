using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health = 5;

    
   
    //time after the death anim starts before the enemy should be destroyed
    public float deathAnimTime = 2;

    //essence to be spawned when enemy is killed
    public GameObject essence;

    public bool dead = false;

    public bool slowToEatFunction = false;
    public bool slowToEat = false;
    public float eatingTime = 1.5f;

    public bool vulnerableToHazards = false;

    //On Collision play damage anim and subtract from health, if health is 0 then start kill coroutine
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //play damage anim
            //myAnim.SetTrigger("Damage");

            health--;
            if(health <= 0)
            {
                StartCoroutine(WaitForKill());
            }
        }

        if (collision.gameObject.CompareTag("Croissant") && slowToEatFunction)
        {
           
            StartCoroutine(WaitForEat(collision.gameObject));
        }

        if (collision.gameObject.CompareTag("Hazzard") && vulnerableToHazards)
        {
         
                StartCoroutine(WaitForKill());
            
        }
    }


    IEnumerator WaitForEat(GameObject croissant)
    {
        //mAnim.SetTrigger("Eat");
        slowToEat = true;
        Destroy(croissant);
        yield return new WaitForSeconds(eatingTime);
        slowToEat = false;
    }


    //start death anim and deactivate all enemy scripts that would cause it to move and whatnot, 
    //wait for the anim to finish then spawn some essence and destroy the enemy
    IEnumerator WaitForKill()
    {
        //myAnim.SetTrigger("Death");
        dead = true;
        yield return new WaitForSeconds(deathAnimTime);
        Instantiate(essence, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
