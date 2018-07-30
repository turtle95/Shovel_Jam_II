using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

    public GameObject turnOn;

    public GameObject[] turnOff;
    public AudioSource player;
    public AudioClip sound;

    public bool quit = false;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!quit)
            {
                player.PlayOneShot(sound);
                turnOn.SetActive(true);

                for (int i = 0; i < turnOff.Length; i++)
                {
                    turnOff[i].SetActive(false);
                }
            }
            else
            {
                Application.Quit();
            }
      
        }
    }
}
