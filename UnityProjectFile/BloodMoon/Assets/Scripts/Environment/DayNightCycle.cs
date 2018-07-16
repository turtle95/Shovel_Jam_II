using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	public bool isNight = false;
	Light sun;

	void Start() {
		sun = gameObject.GetComponent<Light>();
	}

	void Update() {
		if (isNight) {
			sun.enabled = false;
		} else {
			sun.enabled = true;
		}
	}


	public void ChangeToNight () {
		isNight = true;
	}

	public void ChangeToMorning () {
		isNight = false;
	}
}
