using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

    public GameObject turnOn;

    public GameObject[] turnOff;
    public AudioSource player;
    public AudioClip sound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            player.PlayOneShot(sound);
            turnOn.SetActive(true);

            for(int i=0; i<turnOff.Length; i++)
            {
                turnOff[i].SetActive(false);
            }
        }
    }
}
