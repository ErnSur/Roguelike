using UnityEngine;

public abstract class Trigger : MonoBehaviour {

    public abstract State TriggerEvent(Vector3 watcher, Vector3 target);
}
