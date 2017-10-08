using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnSystem  {

    public delegate void EnemyTurn();
    public static EnemyTurn enemyTurn;

    //on enemy turn disable player ability to take action
}
