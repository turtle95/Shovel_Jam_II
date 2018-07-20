using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour {

    public GameObject burst;
   // public GameObject burn;
    public int bulletType = 1;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            switch (bulletType)
            {
                case 1:
                    other.gameObject.GetComponent<spiders>().arrowHit(3);
                    Destroy(this.gameObject);
                    Instantiate(burst, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    break;
                case 2:
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                    Instantiate(burst, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    break;
                default:
                    break;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<spiders>().burning = true;
        }
    }

}
