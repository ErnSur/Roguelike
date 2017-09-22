
using UnityEngine;

public class Interactable : MonoBehaviour {

    //public Transform interactionTransform;

    public Transform player;

    bool hasInteracted = false;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            print("interacted");
            Interact();
            hasInteracted = true;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }
}
