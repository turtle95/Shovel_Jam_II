using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRocks : MonoBehaviour {

    public GameObject shovel;
    public GameObject crossiont;
    public int bulletType = 1;
    public GameObject juiceSpray;
    public GameObject shovelJavelin;
    float shovelCharge = 1;

	public GameObject rock; //rock prefab to spawn
    GameObject rockUsedHere; //gameobject to hold the rock that is created
	public Transform spawnPoint; //spot that the rock is spawned from
	public float rockSize =1;
    public float ammo = 10;
	Rigidbody rb; 
	public float launchSpeed = 100; //launch speed for normal rocks
	public float initialCharge = 100; //initial speed for charge-up rocks  
	public float chargeSpeed = 100; //speed that charge-up rocks gain speed
    public AudioSource audManager;
    public AudioClip shootSound;
//	float chargedLaunch = 0;
    public float rockDisappearTime = 10;

    public Text ammoCount;

    bool loaded = false;
    bool buttonPressed = false;

	// Update is called once per frame
	void Update ()
    {

        buttonPressed = Input.GetButton("Fire1");

        if(buttonPressed || Input.GetAxis("Right Trigger") !=0)
        {
            loaded = true;

            if (bulletType == 2)
                shovelJavelin.SetActive(true);
            else if (bulletType == 3)
                juiceSpray.SetActive(true);
        }


        if (loaded)
        {
            if (bulletType == 2) { }
            if (shovelCharge < 2.5f)
                shovelCharge += Time.deltaTime * 2;

            if (bulletType == 3)
            {
                ammo -= Time.deltaTime;
            }

            if (!buttonPressed && Input.GetAxis("Right Trigger") == 0)
            {
                //If someone presses Fire1 then spawn a rock at the spawn point and give it a launch speed

                switch (bulletType)
                {
                    case 1:
                        audManager.PlayOneShot(shootSound);
                        rockUsedHere = Instantiate(rock, spawnPoint.position, spawnPoint.rotation);
                        rb = rockUsedHere.GetComponent<Rigidbody>();
                        rb.velocity = spawnPoint.forward * launchSpeed;
                        break;
                    case 2:
                        audManager.PlayOneShot(shootSound);
                        rockUsedHere = Instantiate(shovel, shovelJavelin.transform.position, shovelJavelin.transform.rotation);
                        rb = rockUsedHere.GetComponent<Rigidbody>();
                        rb.velocity = spawnPoint.forward * launchSpeed * shovelCharge;
                        shovelCharge = 0;
                        ammo -= 1;
                        shovelJavelin.SetActive(false);
                        break;
                    case 3:
                        //audManager.PlayOneShot(shootSound);
                        juiceSpray.SetActive(false);
                        break;
                }
                loaded = false;
            }
        }



      

           
           
            //audManager.PlayOneShot(shootSound);
			//rockUsedHere = Instantiate (rock, spawnPoint.position, spawnPoint.rotation);
			//rockUsedHere.transform.localScale *= rockSize;
			//rockUsedHere.GetComponent<RockBreakController> ().rockSize = rockSize;
			//rb = rockUsedHere.GetComponent<Rigidbody> ();
			
			//rb.velocity = spawnPoint.forward * launchSpeed;
			//lineDraw.SetActive (false);
		 

        //remove the game obj after x sec
        Destroy(this.rockUsedHere, rockDisappearTime);

        if (ammo <= 0 && (bulletType == 2 || bulletType == 3))
        {
            SwitchBullets(1);
        }

        float tempAmmo = Mathf.Round(ammo);
        ammoCount.text = ("Ammo: " + tempAmmo.ToString());
    }


    public void SwitchBullets(int chosen)
    {
        bulletType = chosen;
        switch (chosen)
        {
            case 1:
                shovelJavelin.SetActive(false);
                juiceSpray.SetActive(false);
              
                break;
            case 2:
                juiceSpray.SetActive(false);
                
                break;
            case 3:
                shovelJavelin.SetActive(false);
               
                break;
        }
    }
}
