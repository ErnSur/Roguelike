﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Elixirs/Poison")]
public class PoisonElixir : Item {

	public Transform poisonFieldPrefab;
	public int poisonDuration = 10;

	public override void OnUsePlayer()
    {
        //Debug.Log(name + " used on player");
		if(PlayerStats.instance.poisonDuration == 0 )
			{
				PlayerStats.instance.poisonDuration += poisonDuration;
				PlayerStats.instance.TakePoisonDamage();
				TurnSystem.nextTurn += PlayerStats.instance.TakePoisonDamage;
			}else
			{
				PlayerStats.instance.poisonDuration += poisonDuration;
			}
    }

    public override void OnUseGround(Vector3 groundPos)
    {
        //Debug.Log(name + " used on ground");
        Instantiate(poisonFieldPrefab, groundPos, Quaternion.identity);
    }

}
