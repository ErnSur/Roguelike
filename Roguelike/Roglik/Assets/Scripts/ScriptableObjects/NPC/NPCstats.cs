using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "NPC/Stats")]
public class NPCstats : ScriptableObject {

    public int maxHp;
    public int currentHp;

    public int attackDamage;
    public int visionRange;

}
