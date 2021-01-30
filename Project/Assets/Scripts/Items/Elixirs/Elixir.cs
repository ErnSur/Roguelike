using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : Item {

	public AudioClip breakSound;

	public override void OnUsePlayer()
    {
        //Debug.Log(name + " used on player");
    }

    public override void OnUseGround(Vector3 groundPos)
    {
        //Debug.Log(name + " used on ground");
    }
}
