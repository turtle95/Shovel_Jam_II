using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySelected : MonoBehaviour {


    public GameObject moon;

	// Use this for initialization
	void Start () {
        StartCoroutine(RunEvents());
	}
	
	IEnumerator RunEvents()
    {
        moon.SetActive(true);
        yield return new WaitForSeconds(2);
    }
}
