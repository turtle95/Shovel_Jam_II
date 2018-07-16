using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHurt : MonoBehaviour
{

    public Collider playerCollider;
    public PlayerHealth playerHealth;
    public float hurtTimeInterval = 1f;

    private bool hurtScreenActive = false;
    private float hurtTimeStart;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hurtScreenActive)
        {
            if (Time.time - hurtTimeStart > hurtTimeInterval)
            {
                //Debug.Log("calling TakeDamage vz");
                playerHealth.TakeDamage(5);
                hurtTimeStart = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("trigger enter vz c:"+c.gameObject.name+" playerCollider:"+playerCollider.gameObject.name);
        if (c == playerCollider)
        {
            //Debug.Log("collider match vz");
            hurtScreenActive = true;
            hurtTimeStart = Time.time;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c == playerCollider)
        {
            hurtScreenActive = false;
        }
    }
}
