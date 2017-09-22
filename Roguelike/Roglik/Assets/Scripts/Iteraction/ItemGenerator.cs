using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    [System.Serializable]
    public struct Spawnable
    {
        public Transform prefab;
        public float chance;
    }

    public Spawnable[] spawnItemsPrefabs;

	// Use this for initialization
	void Start () {

        float tempChance = 0; //
        float roll = Random.value;

        //
        for (int i = 0; i < spawnItemsPrefabs.Length; i++)
        {
            if (roll <= spawnItemsPrefabs[i].chance + tempChance) 
            {
                Instantiate(spawnItemsPrefabs[i].prefab, transform.position, transform.rotation);
                Destroy(gameObject);
                return;
            }
            tempChance += spawnItemsPrefabs[i].chance;
        }
        
        //Default spawn
        Instantiate(spawnItemsPrefabs[0].prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
