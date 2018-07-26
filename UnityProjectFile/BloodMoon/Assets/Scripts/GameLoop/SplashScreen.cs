using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayGame());
	}
	
	IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
