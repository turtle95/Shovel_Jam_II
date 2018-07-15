using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	public void LoadByInt (int SceneInt) {
        SceneManager.LoadScene(SceneInt);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
