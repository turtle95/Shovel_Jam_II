using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    //determines the powerup type
    public int id = 1;
    variableTracker varTrack;


    void Start()
    {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            switch(id)
            {
                case 1: // stamina
                    varTrack.stamina += 5;
                    varTrack.maxStam += 10;
                    break; 

                case 2: // health
                    varTrack.health += 5;
                    varTrack.health += 6;
                    break; 

                case 3: // knowledge
                    varTrack.speed += 1;
                    // gain knowledge information
                    break;
                case 4: //shovelGun
                    other.gameObject.GetComponentInParent<FireRocks > ().bulletType = 2;
                    other.gameObject.GetComponentInParent<FireRocks>().ammo = 5;
                    //gain Knoledge
                    break;
                case 5: //Crossiant Gun
                    other.gameObject.GetComponentInParent<FireRocks>().bulletType = 3;
                    other.gameObject.GetComponentInParent<FireRocks>().ammo = 15;
                    //gain knowledge
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
