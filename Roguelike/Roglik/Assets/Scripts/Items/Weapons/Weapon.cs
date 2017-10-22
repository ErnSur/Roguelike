﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Weapon")]
public class Weapon : Item {

	public int minDamage;
	public int maxDamage;
	//public Upgrade upgrade;
	//public UseEffect effect;

	public override void OnUsePlayer()
	{
		PlayerStats.instance.weapon = this;
		//destroy;
	}
}
