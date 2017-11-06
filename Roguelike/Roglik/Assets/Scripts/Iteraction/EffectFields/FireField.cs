using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : EffectField {

	void Start()
	{
		lifetime = 2;
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "NPC")
		{
			//Debug.Log(other.tag);
			CharacterStats entityStats = other.GetComponent<CharacterStats>();

			if(entityStats.fireDuration == 0 )
			{
				entityStats.fireDuration += 2;
				entityStats.TakeFireDamage();
				TurnSystem.nextTurn += entityStats.TakeFireDamage;
			}else
			{
				entityStats.fireDuration += 1;
			}
		}
	}
}
