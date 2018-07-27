using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfterTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(TurnThisOff());
	}
	
	IEnumerator TurnThisOff()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
