using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySelected : MonoBehaviour {


    public GameObject moon;
    //public AudioClip crashSound;
    public GameObject soundPlayer;
    public CameraShake csScript;
    public GameObject darkness;

	// Use this for initialization
	void Start () {
        StartCoroutine(RunEvents());
	}
	
	IEnumerator RunEvents()
    {
        moon.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(csScript.Shake(1, 0.1f));
        soundPlayer.SetActive(true);
        yield return new WaitForSeconds(1);
        darkness.SetActive(true);
        SceneManager.LoadScene(2);
    }
}
