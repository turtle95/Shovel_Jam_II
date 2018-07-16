using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {


    GameObject [] spawnPoints;
    public GameObject stamina;
    public GameObject health;
    public GameObject knowlege;
    public GameObject shovel;
    public GameObject crossiont;
    public int[] amountToSpawn;
    List<bool> spawnFilled = new List<bool>();

	// Use this for initialization
	void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");

        for (int n = 0; n < spawnPoints.Length; n++)
        {
            spawnFilled.Add(false);
        }
        SpawnDaPowerUps();
	}
	
    void SpawnDaPowerUps()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        for (int n = 0; n < spawnPoints.Length; n++)
        {
            spawnFilled.Add(false);
            if(n < powerUps.Length)
                Destroy(powerUps[n]);
        }

        for (int k = 0; k < 5; k++)
        {
            for (int i = 0; i < amountToSpawn[k]; i++)
            {
                int j;
                do
                {
                    j = Random.Range(0, spawnPoints.Length);
                } while (spawnFilled[j]);


                switch (k)
                {
                    case 0:
                        Instantiate(stamina, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                        spawnFilled[j] = true;
                        break;
                    case 1:
                        Instantiate(health, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                        spawnFilled[j] = true;
                        break;
                    case 2:
                        Instantiate(knowlege, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                        spawnFilled[j] = true;
                        break;
                    case 3:
                        Instantiate(shovel, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                        spawnFilled[j] = true;
                        break;
                    case 4:
                        Instantiate(crossiont, spawnPoints[j].transform.position, spawnPoints[j].transform.rotation);
                        spawnFilled[j] = true;
                        break;
                }
            }
        }
    }

	
}
