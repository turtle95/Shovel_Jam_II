using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteFall : MonoBehaviour {

    public float health = 5f;

    public Transform target;
    public Rigidbody rb;
    public GameObject explosion;

    
	
	// When the satellite dies, fall into the temple
	void Update () {
		if(health <= 0)
        {
            rb.MovePosition(target.position);
        }
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
            //damage flash
        }

        if (collision.gameObject.CompareTag("Temple"))
        {
           
            Instantiate(explosion, collision.contacts[0].point, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }


   
}
