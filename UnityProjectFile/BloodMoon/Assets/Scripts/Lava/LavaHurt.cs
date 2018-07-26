using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHurt : MonoBehaviour
{

    public Collider playerCollider;
    public PlayerHealth playerHealth;
    public int hurtAmount = 5;
    public float hurtTimeInterval = 1f;
    public Stage3Movement mScript;
    private bool hurtScreenActive = false;
    private float hurtTimeStart;

    //Fog and stuff for in blood effects
    public Color fogColor;
    public float fogThickness = 3;
    Color originalFogColor;
    float originalFogThick;

    private void Start()
    {
        originalFogColor = RenderSettings.fogColor;
        originalFogThick = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (hurtScreenActive)
        {
            if (Time.time - hurtTimeStart > hurtTimeInterval)
            {
                //Debug.Log("calling TakeDamage vz");
                playerHealth.TakeDamage(hurtAmount);
                hurtTimeStart = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("trigger enter vz c:"+c.gameObject.name+" playerCollider:"+playerCollider.gameObject.name);
        if (c == playerCollider)
        {
            mScript.gravity = -5;
            //Debug.Log("collider match vz");
            hurtScreenActive = true;
            hurtTimeStart = Time.time;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogThickness;

        }

       
    }

    void OnTriggerExit(Collider c)
    {
        if (c == playerCollider)
        {
            mScript.gravity = -40;
            hurtScreenActive = false;
            RenderSettings.fogColor = originalFogColor;
            RenderSettings.fogDensity = originalFogThick;
        }
    }
}
