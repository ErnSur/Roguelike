using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    [System.Serializable]
    public struct Spawnable
    {
        public Transform prefab;
        [Range(0,1)] public float chance;
    }

    public Spawnable[] spawnItemsPrefabs;
	public Transform defaultItemSpawn;

	// Use this for initialization
	void Start () {

        float tempChance = 0; //
        float roll = Random.value;

        //
        for (int i = 0; i < spawnItemsPrefabs.Length; i++)
        {
            if (roll <= spawnItemsPrefabs[i].chance + tempChance)
            {
                Instantiate(spawnItemsPrefabs[i].prefab, transform.position, transform.rotation, spawnItemsPrefabs[i].prefab.parent);
                Destroy(gameObject);
                return;
            }
            tempChance += spawnItemsPrefabs[i].chance;
        }

        //Default spawn
		if (defaultItemSpawn != null)
		{
        	Instantiate(defaultItemSpawn, transform.position, transform.rotation, spawnItemsPrefabs[0].prefab.parent);
		}
        Destroy(gameObject);
    }
}
