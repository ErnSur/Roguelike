﻿using UnityEngine;

public class EffectField : MonoBehaviour {

	public ParticleSystem ps;
	public Collider2D colliderBox;
	public int lifetime = 5;

	public virtual void OnTriggerEnter2D(Collider2D other){
		//code
	}

	public virtual void Timer()
	{
		lifetime--;
		if ( lifetime <= 0 )
		{
			ps.Stop();
			colliderBox.enabled = false;
		}
	}

	void OnEnable()
	{
		TurnSystem.nextTurn += Timer;
	}

	void OnDisable()
	{
		TurnSystem.nextTurn -= Timer;
	}

	void Update()
	{
		if ( lifetime <= 0 )
		{
			transform.localScale =  Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 4);
		}
	}
	void Start()
	{
		ps = GetComponent<ParticleSystem>();
		colliderBox = GetComponent<Collider2D>();
		//ps.colorOverLifetime.color.a += 1;
	}
}
