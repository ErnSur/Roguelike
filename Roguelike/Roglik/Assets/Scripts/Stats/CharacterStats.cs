using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour {

#region /// Basic Statistics ///
    public int maxHealth = 60;
    public int currentHealth;
    public float visionRange = 6;
    public int attackDamage = 2;
    public int AttackDamage
	{
		get{
			if (weapon != null)
			{
				int att = Random.Range(weapon.minDamage, weapon.maxDamage) + attackDamage;
				//Log.Write(this.name + " attacked For: " + att);
				return att;
			} else { return attackDamage; }
		}
	}
#endregion
#region /// Weapon & Hit Effects ///
	public Weapon weapon;
	public List<Stone> stones;
#endregion
#region /// Coordination ///
	private Vector3 position;
	public Vector3 previousPosition;
	public Vector3 Position{
		get{ return position; }
		set{ previousPosition = position;
			position = value;
			previousNode = node;
			previousNode.walkable = true;
			node = PFgrid.grid[(int)position.x,(int)position.y];
			node.walkable = false;
		}
	}
	public PFnode node;
	PFnode previousNode;
#endregion
#region /// Status Effects ///
	public int poisonDuration = 0;
	public int poisonDamage = 5;
	public int fireDuration = 0;
	public int fireDamage = 15;
	public int paralyzeDuration;
#endregion
#region /// Audio & Visuals ///
    public AudioClip[] hurtSound;
    private AudioSource audioSrc;
    private SpriteRenderer sprite;
#endregion

	public void Heal ( int amount )
	{
		currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
		StartCoroutine("ColorFlicker", Color.green);
	}

	/// TAKING DAMAGE TYPES /// //add TakeEffectDamage to fixedUpdate and just increese effectduration on npcs?
    public void TakeDamage(int damage) // (int damage, upgrade effect)
    {
		//effect.act();
        //damage -= armor.GetValue();
        audioSrc.clip = hurtSound[Random.Range(0,hurtSound.Length)]; //maybe do it on corutine or dont destroy object, just change its sprite and disable states
        audioSrc.Play();

        currentHealth -= damage;
		StartCoroutine("ColorFlicker", Color.red);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

	public void TakePoisonDamage()
	{
		if(poisonDuration > 0 )
		{
			TakeDamage(poisonDamage);
			//Debug.Log(this.name + " took:" + poisonDamage + " poison damage.");
			poisonDuration--;
		}else
		{
			TurnSystem.nextTurn -= TakePoisonDamage;
		}
	}

	public void TakeFireDamage()
	{
		if(fireDuration > 0 )
		{
			TakeDamage(fireDamage);
			//Debug.Log(this.name + " took:" + poisonDamage + " poison damage.");
			fireDuration--;
		}else
		{
			TurnSystem.nextTurn -= TakeFireDamage;
		}
	}

	/// DEAL DAMAGE ///
	public void DealDamage(CharacterStats enemy) //effect eff
	{
		int blow = AttackDamage;

		enemy.TakeDamage(blow);
		Log.Write(this.name + " attacked For: " + blow);

		if (weapon.onHitEffect != null)
		{
			weapon.onHitEffect.OnHitEffect(enemy);
			Log.Write("Poisoned");
		}
		// ADD RAYCASTING HERE TO MODIFY IT WITH RED GEM

		//if (eff != null) {	eff.function(enemy);	}
	}

/*
	public void InflictPoison(int duration)
	{
		Poison.duration += duration;
		TurnSystem.nextTurn += TakePoisonDamage;
	}
	public void TakeDamageType(DamageType damage, int duration)
	{
		DamageType dmg =
		Poison.duration += duration;
		TurnSystem.nextTurn += TakeTypeDamage<T>;
	}

	private void TakeTypeDamage<T>()
	{
		if(T.duration >0)
		{
			TakeDamage(Poison.dmg);
			Poison.duration--;
		}else
		{
			TurnSystem.nextTurn -= TakePoisonDamage;
		}
	}
	private void TakePoisonDamage()
	{
		if(Poison.duration >0)
		{
			TakeDamage(Poison.dmg);
			Poison.duration--;
		}else
		{
			TurnSystem.nextTurn -= TakePoisonDamage;
		}
	}
*/
    public virtual void Die()//Player object cannot be destroyed, light and audio systems are attached
    {
        //die in some way
        //Debug.Log(transform.name + " died.");
		node.walkable = true;
		Destroy(gameObject);
    }

    IEnumerator ColorFlicker(Color toColor)
    {
        bool changed = false;
        float t = 0;

        while (!changed)
        {
            t += 0.2f;
            //sprite.color = Color.Lerp(Color.white, Color.red, (ElapsedTime / TotalTime));
            sprite.color = Color.Lerp(Color.white, toColor, Mathf.PingPong(t,1)); //Mathf.PingPong(Time.time * 1f, 1.0f)

            if (sprite.color == Color.white)
            {
                changed = true;
            }
            yield return null;
        }
    }

    public void Start()
    {
        currentHealth = maxHealth;
		position = transform.position;
		previousPosition = transform.position;
		node = PFgrid.grid[(int)position.x,(int)position.y];
		node.walkable = false;
        sprite = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }
}
