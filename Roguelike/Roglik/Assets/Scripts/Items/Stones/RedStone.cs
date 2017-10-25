using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Stone/Red")]
public class RedStone : Stone {

	public Transform poisonFieldPrefab;
	public Vector3 correction;

	public int range = 5;
	public bool[] cords;


	void OnEnable()
	{
		Debug.Log("RedStone Enabled");
		if(cords.Length < (range * range))
		{
			cords = new bool[range * range];
		}
	}
	void OnDisable()
	{
		Debug.Log("RedStone disabled");
	}

	int t = 0;
	public void SpawnField(Vector3 playerPos)
	{
		for (int x = 0; x < range; x++ )
		{
			for (int y = 0; y < range; y++ )
			{
				if (cords[t])
				{
					Vector3 spawnPos = new Vector3(x,-y,0);
					spawnPos += playerPos;
					spawnPos += correction;

					Instantiate(poisonFieldPrefab, spawnPos, Quaternion.identity);
				}
				t++;
			}
		}
		t = 0;
	}
}
