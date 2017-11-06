using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Stone/Red")]
public class RedStone : Stone {

	public Transform poisonFieldPrefab;
	public Vector3 playerOnMiniGrid;

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
	public void SpawnField(Vector3 playerPos, string direction) // direction as enum
	{
		for (int y = 0; y < range; y++ )
		{
			for (int x = 0; x < range; x++ )
			{
				if (cords[t])
				{
					/// TRANSLATION
					int nx = x;
					int ny = y;

					/// ROTATION
					switch(direction)
					{
						case "up":
							nx = y;
							ny = x;
						break;
						case "down":
							nx = 2*(int)playerOnMiniGrid.y - y; //+ (int)playerOnMiniGrid.y;
							ny = 2*(int)playerOnMiniGrid.x - x; //+ (int)playerOnMiniGrid.y;
						break;
						case "left":
							nx = 2*(int)playerOnMiniGrid.x - x;
							ny = 2*(int)playerOnMiniGrid.y - y;
						break;
						case "right":
							nx = x;
							ny = y;
						break;
						default:
							nx = x;
							ny = y;
						break;
					}

					Vector3 spawnPos = new Vector3((nx - playerOnMiniGrid.x),(ny - playerOnMiniGrid.y),0);
					spawnPos += playerPos;

					Instantiate(poisonFieldPrefab, spawnPos, Quaternion.identity);

					/*
					Vector3 playerPos3 = PlayerStats.instance.Position;

					Vector3 spawnPos = new Vector3((playerOnMiniGrid.x - nx)+playerPos3.x,playerPos3.y-(playerOnMiniGrid.y - ny),0);
					*/
				}
				t++;
			}
		}
		t = 0;
	}
}
