using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHole : MonoBehaviour {

    public GameObject galaxy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            galaxy.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
