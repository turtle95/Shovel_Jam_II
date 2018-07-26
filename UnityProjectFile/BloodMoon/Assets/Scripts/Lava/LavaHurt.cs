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

    //grabs the normal fog settings so it can reset it later
    private void Start()
    {
        originalFogColor = RenderSettings.fogColor;
        originalFogThick = RenderSettings.fogDensity;
    }

   
    void Update()
    {
        //hurts ya if you are in the water
        if (hurtScreenActive)
        {
            if (Time.time - hurtTimeStart > hurtTimeInterval)
            {
                playerHealth.TakeDamage(hurtAmount);
                hurtTimeStart = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //turns down gravity, starts hurting you, adds underwater fog
        if (c == playerCollider)
        {
            mScript.gravity = -5;
            hurtScreenActive = true;
            hurtTimeStart = Time.time;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogThickness;

        }

       
    }
    //turns gravity back up, resets the fog, stops hurting you
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
