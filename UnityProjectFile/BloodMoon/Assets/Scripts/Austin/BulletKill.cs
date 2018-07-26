﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour {

    //blood burst for the spiders
    public GameObject burst;

    //checks if this is a shovel or a croissant
    public int bulletType = 1;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            switch (bulletType)
            {
                case 1:
                    other.gameObject.GetComponent<spiders>().arrowHit(3);
                    Destroy(this.gameObject);
                    Instantiate(burst, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    break;
                case 2:
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                    Instantiate(burst, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    break;
                default:
                    break;
            }
            
        }
    }

    //For the croissant flame thrower, I'm thinking we should drop this one
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<spiders>().burning = true;
        }
    }

}
