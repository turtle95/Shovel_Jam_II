using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {


     GameObject [] spawnPoints;
    public GameObject stamina;
    public GameObject health;
    public GameObject knowlege;

	// Use this for initialization
	void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        for(int i =0; i < spawnPoints.Length; i++)
        {
            int j = Random.Range(0, 2);
            switch (j)
            {
                case 0: Instantiate(stamina, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    break;
                case 1: Instantiate(health, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    break;
                case 2:
                    Instantiate(knowlege, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
