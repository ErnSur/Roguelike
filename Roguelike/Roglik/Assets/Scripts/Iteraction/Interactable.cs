using UnityEngine;

public class Interactable : MonoBehaviour {

    bool hasInteracted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Interact();
            hasInteracted = true;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }
}
