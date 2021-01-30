using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/WeaponEffect")]
public class WeaponEffect : ScriptableObject {

	public enum EffectType
	{
		POISON,
		FIRE
	};

	///icon for visual effect

	public EffectType effect;

	public void OnHitEffect(CharacterStats enemy)
	{
		switch (effect)
		{
			case EffectType.POISON:
				//chance to add poison duration
				enemy.poisonDuration += 2; //cant have more than one takepoison subscribed
				//TurnSystem.nextTurn += enemy.TakePoisonDamage;
				break;
			case EffectType.FIRE:
				enemy.fireDuration += 2;
				//TurnSystem.nextTurn += enemy.TakeFireDamage;
				break;
			default:
				break;
		}
	}
}
