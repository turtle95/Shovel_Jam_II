﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour {

	public float gravity = -10;

    variableTracker varTrack;
    Vector3 gravityUp;

    private void Start()
    {
        varTrack = GameObject.Find("variableTracker").GetComponent<variableTracker>();
    }

    //calculates and applies planet based gravity, can be called and used by anything!
    public void Attract(Transform turdNugget, Transform downLooker)
	{
        if (varTrack.outsidePlanet)
        {
            gravityUp = (transform.position - turdNugget.position).normalized;
        }
        else
        {
            gravityUp = (turdNugget.position - transform.position).normalized;
        }
		
		Vector3 turdsUp = downLooker.up;

		turdNugget.GetComponent<Rigidbody>().AddForce(turdsUp * gravity);

		Quaternion targetRotation = Quaternion.FromToRotation (turdsUp, gravityUp) * downLooker.rotation;
		downLooker.rotation = Quaternion.Slerp (downLooker.rotation, targetRotation, 50 * Time.deltaTime);
	}
}
