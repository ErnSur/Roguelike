using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	#region Variables
	public SpriteRenderer sprite;
	public PFnode node;
	public AudioSource audioSource;
	#endregion

	#region MonoMethods
	void OnMouseUp()
	{
		node.walkable = !node.walkable;
		sprite.flipX = !sprite.flipX;
		if(sprite.flipX)
		{
			audioSource.Play();
			transform.Rotate(new Vector3(0,0,90));
		}else
		{
			audioSource.Play();
			transform.Rotate(new Vector3(0,0,270));
		}
		//Debug.Log("door: "+ node.walkable);
	}
	void Start()
	{
		node = PFgrid.grid[(int)transform.position.x,(int)transform.position.y];
		audioSource = GetComponent<AudioSource>();
	}
	#endregion
}
