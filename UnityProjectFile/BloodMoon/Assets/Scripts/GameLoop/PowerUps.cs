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
                case 1: break;

                case 2: break;

                case 3: break;
            }
            Destroy(this.gameObject);
                    }
    }
}
