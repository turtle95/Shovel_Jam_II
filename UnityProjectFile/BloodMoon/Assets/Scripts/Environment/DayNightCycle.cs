using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	public bool isNight = false;
	Light sun;
    public GameObject glowies;
	void Start() {
		sun = gameObject.GetComponent<Light>();
	}

	//void Update() {
	//	if (isNight) {
	//		sun.enabled = false;
 //           glowies.SetActive(true)
	//	} else {
	//		sun.enabled = true;
	//	}
	//}


	public void ChangeToNight () {
		isNight = true;
        sun.enabled = false;
        glowies.SetActive(true);
        DynamicGI.UpdateEnvironment();
	}

	public void ChangeToMorning () {
		isNight = false;
        sun.enabled = true;
        glowies.SetActive(false);
        DynamicGI.UpdateEnvironment();
	}
}
