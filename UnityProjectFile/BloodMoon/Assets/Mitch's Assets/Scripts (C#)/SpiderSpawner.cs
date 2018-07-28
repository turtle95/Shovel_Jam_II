using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{

    [SerializeField] private LayerMask everythingButTerrain;
    public Transform planet;
    public GameObject player;
    [SerializeField] private GameObject spiderPrefab;
    public bool trickleOnStart = true;
    public bool stopTrickle = false;
    public int numberToTrickle = 20;
    public int numberToMassSpawn = 80;
    public int spiderCountCurrent =0;
    private List<GameObject> spiderTrickleList = new List<GameObject>();
    private List<GameObject> spiderMassList = new List<GameObject>();
    private bool clearingList = false;
    //[HideInInspector] public enum Clear { All, Trickle, Mass };
    private bool massSpawning = false;


    //variable to give the spiders a size range
    float sizeRange;


    // Use this for initialization
    void Start()
    {
        if (trickleOnStart == true) StartCoroutine(TrickleSpawn());
        InvokeRepeating("CheckTrickleNumbers", 5f, 30f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckTrickleNumbers()
    {
        //Remove Null (destroyed) spiders
        for (var i = spiderTrickleList.Count - 1; i > -1; i--)
        {
            if (spiderTrickleList[i] == null)
                spiderTrickleList.RemoveAt(i);
        }

        if (spiderTrickleList.Count < numberToTrickle - 4)
        {
            StartCoroutine(TrickleSpawn());
        }
    }

    private IEnumerator TrickleSpawn()
    {
        if (stopTrickle == true)
        {
            CancelInvoke("CheckTrickleNumbers");
        }

        while (spiderCountCurrent < numberToTrickle)
        {
            // cast a random ray to see if we hit land
            Vector3 randomPoint = Random.onUnitSphere * 10; //this is an imaginary sphere that will choose a random point to cast a ray from.
            Vector3 pointAwayFromPlanet = -300f * Vector3.Normalize(planet.position - randomPoint) + randomPoint; //find a point along the vector of planet.position and randomPoint but 300 units out.
            RaycastHit hit;
            if (Physics.Linecast(randomPoint, pointAwayFromPlanet, out hit))
            {
                //if (hit.transform.root.tag != "Planet")
                //    yield break;

                //Debug.DrawLine(randomPoint, hit.point, Color.red, 20f);

                // we hit a spawnable area
                Vector3 pointAboveGround = 1f * Vector3.Normalize(randomPoint - hit.point) + hit.point; //!! hit.point isn't always on ground
                GameObject obj = Instantiate(spiderPrefab, pointAboveGround, Quaternion.identity);
                sizeRange = Random.Range(1, 10);
                obj.GetComponent<Transform>().localScale = new Vector3(sizeRange, sizeRange, sizeRange);
                spiders spiderScript = obj.GetComponent<spiders>();
                spiderScript.planet = planet; //the planet that these spiders belong to
                spiderScript.target = player.transform;

                spiderTrickleList.Add(obj);
                spiderCountCurrent++;
            }
            yield return null;
        }
    }

    public void MassSpawn()
    {
        if (massSpawning == false) StartCoroutine(MassSpawning());
    }

    private IEnumerator MassSpawning()
    {
        massSpawning = true;
        while (spiderCountCurrent < numberToMassSpawn)
        {
            // cast a random ray to see if we hit land
            Vector3 randomPoint = Random.onUnitSphere * 100; //this is an imaginary sphere that will choose a random point to cast a ray from.
            Vector3 pointAwayFromPlanet = -300f * Vector3.Normalize(planet.position - randomPoint) + randomPoint; //find a point along the vector of planet.position and randomPoint but 300 units out.
            RaycastHit hit;
            if (Physics.Linecast(randomPoint, pointAwayFromPlanet, out hit))
            {
                //if (hit.transform.root.tag != "Planet")
                //    yield break;

                Debug.DrawLine(randomPoint, hit.point, Color.red, 20f);

                // we hit a spawnable area
                Vector3 pointAboveGround = 1f * Vector3.Normalize(randomPoint - hit.point) + hit.point; //!! hit.point isn't always on ground
                GameObject obj = Instantiate(spiderPrefab, pointAboveGround, Quaternion.identity);
                sizeRange = Random.Range(1, 20);
                obj.GetComponent<Transform>().localScale = new Vector3(sizeRange, sizeRange, sizeRange);
                spiders spiderScript = obj.GetComponent<spiders>();
                spiderScript.planet = planet; //the planet that these spiders belong to
                //spiderScript.target = player.transform;

                spiderMassList.Add(obj);
                spiderCountCurrent++;
            }
            yield return null;
        }
        massSpawning = false;
    }

    public void ClearSpiders(bool clearAll)
    {
        if (clearAll == true)
        {
            if (clearingList == false) StartCoroutine(DestroyAllSpiders());
        }
        else
        {
            if (clearingList == false) StartCoroutine(DestroyMassSpiders());
        }

        //switch (clearState)
        //{
        //    case Clear.All:
        //        if (clearingList == false) StartCoroutine(DestroyAllSpiders());
        //        break;
        //    case Clear.Mass:

        //        break;
        //    case Clear.Trickle:

        //        break;
        //}
    }

    private IEnumerator DestroyAllSpiders()
    {
        clearingList = true;

        for (int i = spiderTrickleList.Count - 1; i >= 0; i--)
        {
            Destroy(spiderTrickleList[i]);
            spiderCountCurrent--;
            spiderTrickleList.Remove(spiderTrickleList[i]);
            yield return null;
        }
        spiderTrickleList.Clear();

        for (int i = spiderMassList.Count - 1; i >= 0; i--)
        {
            Destroy(spiderMassList[i]);
            spiderCountCurrent--;
            spiderTrickleList.Remove(spiderMassList[i]);
            yield return null;
        }
        spiderMassList.Clear();

        clearingList = false;
    }

    private IEnumerator DestroyMassSpiders()
    {
        clearingList = true;

        for (int i = spiderMassList.Count - 1; i >= 0; i--)
        {
            Destroy(spiderMassList[i]);
            spiderCountCurrent--;
            spiderTrickleList.Remove(spiderMassList[i]);
            yield return null;
        }
        spiderMassList.Clear();

        clearingList = false;
    }
}
