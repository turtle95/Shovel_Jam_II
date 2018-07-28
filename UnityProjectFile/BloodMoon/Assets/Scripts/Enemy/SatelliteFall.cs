using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SatelliteFall : MonoBehaviour {

    public float health = 5f;

    public Transform target;
    public Rigidbody rb;
    public GameObject explosion;

    EventManager eScript;

    public GameObject announcer;
    public Text announceText;

    private void Start()
    {
        eScript = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // When the satellite dies, fall into the temple
    void Update () {
		if(health <= 0)
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
            eScript.bossCount--;
            announcer.SetActive(true);
            announceText.text = "Temple Destroyed!" + eScript.bossCount + " left to decimate.";
           
            Instantiate(explosion, collision.contacts[0].point, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }


   
}
