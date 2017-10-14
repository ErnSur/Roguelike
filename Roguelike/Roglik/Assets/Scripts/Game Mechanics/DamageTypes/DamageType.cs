using UnityEngine;

public abstract class DamageType {

}

public class Poison : DamageType {

	public static int damage = 5;
	public int duration;

	public static void DealDamage(CharacterStats entity)
    {
		if ( entity.poisonDuration > 0 )
		{
			entity.TakeDamage(damage);
			entity.poisonDuration--;
		}else
		{
			//TurnSystem.nextTurn -= DealDamage(entity);
		}
    }
}
