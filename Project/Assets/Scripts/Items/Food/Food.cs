using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Food/BasicFood")]
public class Food : Item {

	public int healAmount;

	public override void OnUsePlayer()
    {
        //Debug.Log(name + " used on player");
		PlayerStats.instance.Heal(healAmount);
    }
}
