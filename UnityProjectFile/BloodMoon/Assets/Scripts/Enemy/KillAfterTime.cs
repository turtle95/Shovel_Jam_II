using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KillTime());
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    IEnumerator KillTime()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
