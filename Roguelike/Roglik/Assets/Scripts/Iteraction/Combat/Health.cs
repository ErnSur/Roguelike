using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHp;
    public int currentHp;

	
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        print(currentHp);
    }

    void Start () {
        currentHp = maxHp;
	}

    private void Update()
    {
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
