using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBosses : MonoBehaviour {

    public GameObject arrow;
    public Transform Aimer;
    public Transform playerTrans;
    bool inSights = false;

	
	
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
            StartCoroutine(FireRate());
            inSights = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        inSights = false;
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(arrow, Aimer.position, Aimer.rotation);
        if (inSights)
            StartCoroutine(FireRate());
    }

}
