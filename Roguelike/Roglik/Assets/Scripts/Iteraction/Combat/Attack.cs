using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public int attackPower;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        print(collision.tag);
        collision.GetComponent<Health>().TakeDamage(attackPower);
    }
}
