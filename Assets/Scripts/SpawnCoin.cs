using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject prefabMoeda;
    public Transform spawn1, spawn2, spawn3, spawn4;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Spawn()); // ou ("Spawn");		
	}
	
    IEnumerator Spawn()
    {
        int rand = Random.Range(1, 5);
        print("random: " + rand);

        switch (rand)
        {
            case 1:
                GameObject tempPrefab = Instantiate(prefabMoeda, spawn1.transform.position, spawn1.transform.rotation) as GameObject;                
                break;
            case 2:
                GameObject tempPrefab2 = Instantiate(prefabMoeda, spawn2.transform.position, spawn2.transform.rotation) as GameObject;
                break;
            case 3:
                GameObject tempPrefab3 = Instantiate(prefabMoeda, spawn3.transform.position, spawn3.transform.rotation) as GameObject;
                break;
            case 4:
                GameObject tempPrefab4 = Instantiate(prefabMoeda, spawn4.transform.position, spawn4.transform.rotation) as GameObject;
                break;
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(Spawn()); // ou ("Spawn");		
    }
}