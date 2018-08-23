using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {

    public Animator myAnim;
    public Animator damageAnim;

    public string awakenTrigger = "Awaken";
    public string hideTrigger = "Hide";
    public string eatTrigger = "Eat";
    public string attackTrigger = "Attack";
    public string idleTrigger = "Idle";
    public string chargeTrigger = "Charge";
    public string lungeTrigger = "Lunge";
    public string runTrigger = "Run";
    public string deathTrigger = "Death";
    public string damageTrigger = "Damage";


    EnemyHealth healthScript;
    EnemyAttack attackScript;
    EnemySight sightScript;

    public int currentAnim = 0;

    // Use this for initialization
    void Start () {
        healthScript = GetComponent<EnemyHealth>();
        attackScript = GetComponent<EnemyAttack>();
        sightScript = GetComponent<EnemySight>();
	}
	
	
	void Update () {
        if (sightScript.attacking)
        {
            if (attackScript.lunge)
                currentAnim = 4;
            else if (attackScript.charge)
                currentAnim = 3;
            else
                currentAnim = 2;
        }
        else if (sightScript.targetAquired)
        {
            currentAnim = 1;
        }
        else
            currentAnim = 0;

        PlayAnAnim();
	}

    void PlayAnAnim()
    {
        switch (currentAnim)
        {
            case 0:
                myAnim.SetTrigger(idleTrigger);
                break;
            case 1:
                myAnim.SetTrigger(runTrigger);
                break;
            case 2:
                myAnim.SetTrigger(attackTrigger);
                break;
            case 3:
                myAnim.SetTrigger(chargeTrigger);
                break;
            case 4:
                myAnim.SetTrigger(lungeTrigger);
                break;
            case 5:
                myAnim.SetTrigger(deathTrigger);
                break;
            case 6:
                myAnim.SetTrigger(eatTrigger);
                break;
            case 7:
                myAnim.SetTrigger(awakenTrigger);
                break;
            case 8:
                myAnim.SetTrigger(hideTrigger);
                break;
        }
        myAnim.SetTrigger(eatTrigger);
    }

    public void TakeDamage()
    {
        damageAnim.SetTrigger(damageTrigger);
    }
}
