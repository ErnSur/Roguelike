using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStone : Stone {

	public enum EffectType
	{
		POISON,
		FIRE
	};

	public StoneType type;
	public EffectType effect;

	public void OnHitEffect(CharacterStats enemy)
	{
		switch (effect)
		{
			case EffectType.POISON:
				//chance to add poison duration
				enemy.poisonDuration += 2;
				TurnSystem.nextTurn += enemy.TakePoisonDamage;
				break;
			case EffectType.FIRE:
				enemy.fireDuration += 2;
				TurnSystem.nextTurn += enemy.TakeFireDamage;
				break;
			default:
				break;
		}
	}
}
