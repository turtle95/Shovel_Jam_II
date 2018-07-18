using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour {

    public GameObject burst;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(burst, other.gameObject.transform.position, other.gameObject.transform.rotation);

            Destroy(other.gameObject);

           
            //other.gameObject.GetComponent<spiders>().arrowHit(2);
            Destroy(this.gameObject);
        }
    }

}
