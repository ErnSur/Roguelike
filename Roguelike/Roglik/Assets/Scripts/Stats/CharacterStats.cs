﻿using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

#region /// Basic Statistics ///
    public int maxHealth = 60;
    public int currentHealth;
    public int attackDamage;
    public float visionRange = 6;
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
	public int fireDuration;
	public int FireDamage;
	public int paralyzeDuration;
#endregion
#region /// Audio & Visuals ///
    public AudioClip[] hurtSound;
    private AudioSource audioSrc;
    private SpriteRenderer sprite;
#endregion


	/// TAKING DAMAGE TYPES ///
    public void TakeDamage(int damage)
    {
        //damage -= armor.GetValue();
        audioSrc.clip = hurtSound[Random.Range(0,hurtSound.Length)];
        audioSrc.Play();

        currentHealth -= damage;
        StartCoroutine("ColorFlicker");

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
		//Destroy(gameObject);
    }

    IEnumerator ColorFlicker()
    {
        bool changed = false;
        float t = 0;

        while (!changed)
        {
            t += 0.2f;
            //sprite.color = Color.Lerp(Color.white, Color.red, (ElapsedTime / TotalTime));
            sprite.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(t,1)); //Mathf.PingPong(Time.time * 1f, 1.0f)

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
        sprite = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }
}
