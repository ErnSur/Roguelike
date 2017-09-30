using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public bool attackMode;

    public PlayerStats stats;

    public Attack attackPrefab;

	// Use this for initialization
	void Start () {
        stats = GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Attack att = Instantiate(attackPrefab, gameObject.transform.position + Vector3.right, Quaternion.identity);
                att.attackPower = stats.dmg;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Attack att = Instantiate(attackPrefab, gameObject.transform.position + Vector3.left, Quaternion.identity);
                att.attackPower = stats.dmg;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Attack att = Instantiate(attackPrefab, gameObject.transform.position + Vector3.up, Quaternion.identity);
                att.attackPower = stats.dmg;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Attack att = Instantiate(attackPrefab, gameObject.transform.position + Vector3.down, Quaternion.identity);
                att.attackPower = stats.dmg;
            }
    }
}
