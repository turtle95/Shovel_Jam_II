using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    //determines the powerup type
    public int id = 1;

	

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            switch(id)
            {
                case 1: // stamina
                    // gain 5 stamina
                    // gain 10 max stamina
                    break; 

                case 2: // health
                    // gain 5 health
                    // gain 6 max health
                    break; 

                case 3: // knowledge
                    // gain 1 speed
                    // gain knowledge information
                    break; 
            }
            Destroy(this.gameObject);
        }
    }
}
