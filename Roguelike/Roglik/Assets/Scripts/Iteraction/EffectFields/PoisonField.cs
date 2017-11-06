using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonField : EffectField {

	void Start()
	{
		lifetime = 3;
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "NPC")
		{
			//Debug.Log(other.tag);
			CharacterStats entityStats = other.GetComponent<CharacterStats>();

			if(entityStats.poisonDuration == 0 )
			{
				entityStats.poisonDuration += 4;
				entityStats.TakePoisonDamage();
				TurnSystem.nextTurn += entityStats.TakePoisonDamage;
			}else
			{
				entityStats.poisonDuration += 4;
			}
		}
	}
}
