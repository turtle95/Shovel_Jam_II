using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

    public GameObject turnOn;

    public GameObject[] turnOff;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            turnOn.SetActive(true);

            for(int i=0; i<turnOff.Length; i++)
            {
                turnOff[i].SetActive(false);
            }
        }
    }
}
