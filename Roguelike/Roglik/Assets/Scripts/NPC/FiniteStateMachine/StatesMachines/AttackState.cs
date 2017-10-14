using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {


    public override void Act()
    {
        PlayerStats.instance.TakeDamage(stats.attackDamage);
        //Debug.Log("attacked player for: " + stats.attackDamage);
        UpdateState();
    }
}
