using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

    public AudioSource stepSound;
    public float speed = 3.5f;
    public LayerMask wallLayer;

    private float raycastDistance = 1f;
    public Vector2 dir;
    private Vector3 pos;
    private Transform tr;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D) && tr.position == pos && !RayWallUpdate(Vector3.right))
        {
            pos += Vector3.right;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.A) && tr.position == pos && !RayWallUpdate(Vector3.left))
        {
            pos += Vector3.left;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.W) && tr.position == pos && !RayWallUpdate(Vector3.up))
        {
            pos += Vector3.up;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.S) && tr.position == pos && !RayWallUpdate(Vector3.down))
        {
            pos += Vector3.down;
            stepSound.Play();
        }

        //rb.velocity = Vector3.ClampMagnitude(pos, 1f) * speed;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    bool RayWallUpdate(Vector2 rayDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, raycastDistance, wallLayer);
        Debug.DrawRay(transform.position, rayDirection, Color.green, 1f);
        
        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }
}
