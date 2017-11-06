using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSystem {

	public static void IncreaseFear(int amount)
	{
		PlayerStats player = PlayerStats.instance;

		player.currentFear = Mathf.Clamp(player.currentFear + amount,0,player.maxFear);
		if ( player.currentFear == player.maxFear )
		{
			player.TakeDamage(amount);
		}
	}

	public static void IncreaseFearInDark()
	{
		if (!PlayerTorch.torch)
		{
			IncreaseFear(1);
		}
	}
}
