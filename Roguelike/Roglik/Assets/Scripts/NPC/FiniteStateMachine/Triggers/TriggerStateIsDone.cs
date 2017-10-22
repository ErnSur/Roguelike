using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trigger/State Is Done")]
public class TriggerStateIsDone : Trigger {

	public override State TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone)
	{
		if(isStateDone)
		{
			return stateToReturn;
		}
		return null;
	}
}