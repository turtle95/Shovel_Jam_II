using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFade : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        StartCoroutine(FadeOut());
	}
	
	IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
