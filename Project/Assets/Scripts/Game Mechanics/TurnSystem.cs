using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnSystem  {

    public delegate void Turn();
    public static Turn nextTurn;
	//errors might be shown when nothing is subscribed to delegate
}
