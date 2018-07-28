using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSatelliteFall : MonoBehaviour {

    public float health = 5f;

    public Transform target;
    public Rigidbody rb;
    public GameObject explosion;

    public GameObject parts;

    // When the satellite dies, fall into the temple
    void Update()
    {
        if (health <= 0)
        {
            transform.LookAt(target.position);
            rb.AddForce(transform.forward * 5000, ForceMode.Force);
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
            Instantiate(parts, collision.contacts[0].point, collision.transform.rotation);
            Instantiate(explosion, collision.contacts[0].point, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
