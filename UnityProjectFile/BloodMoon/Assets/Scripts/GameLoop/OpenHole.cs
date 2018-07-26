using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHole : MonoBehaviour {

    public GameObject galaxy;

    //Destroys the temple and makes a whole in the planet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            galaxy.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
