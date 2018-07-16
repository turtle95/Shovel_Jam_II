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
                    varTrack.AddStamina();
                    break; 

                case 2: // health
                    varTrack.AddHealth();
                    break; 

                case 3: // knowledge
                    varTrack.AddSpeed();
                    // gain knowledge information
                    break;
                case 4: //shovelGun
                    varTrack.GrabWeapon(1);
                    other.gameObject.GetComponentInParent<FireRocks>().SwitchBullets(2);
                    other.gameObject.GetComponentInParent<FireRocks>().ammo = 5;
                    //gain Knoledge
                    break;
                case 5: //Crossiant Gun
                    varTrack.GrabWeapon(2);
                    other.gameObject.GetComponentInParent<FireRocks>().SwitchBullets(3);
                    other.gameObject.GetComponentInParent<FireRocks>().ammo = 15;
                    //gain knowledge
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
