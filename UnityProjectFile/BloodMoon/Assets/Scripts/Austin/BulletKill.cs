using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DestroyObject(other.gameObject);
        }
    }

}
