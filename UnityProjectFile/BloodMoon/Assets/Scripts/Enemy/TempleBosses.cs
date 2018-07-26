using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBosses : MonoBehaviour {

    public GameObject arrow;
    public Transform Aimer;
    public Transform playerTrans;
    bool inSights = false;
    Rigidbody rb;
    public float launchSpeed = 50;
	
	// Update is called once per frame
	void Update () {
        if (inSights)
        {
            Aimer.LookAt(playerTrans.position);

        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrans = other.gameObject.transform;
            //if(!inSights)
            StartCoroutine(FireRate());
            inSights = true;
        }

    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inSights = false;
        }
    }

    IEnumerator FireRate()
    {
        rb = Instantiate(arrow, Aimer.position, Aimer.rotation).GetComponent<Rigidbody>();
        rb.velocity = Aimer.forward * launchSpeed;
        yield return new WaitForSeconds(0.5f);

        if (inSights)
            StartCoroutine(FireRate());
    }

}
