using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public AudioSource stepSound;
    public float speed = 3.5f;
    private Vector3 pos;
    private Transform tr;

    Rigidbody2D rb;
    public float raycastMaxDistance = 2f;
    public Vector2 dir;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
        tr = transform;
    }
	
	// Update is called once per frame
	void Update () {
        


        if (Input.GetKey(KeyCode.D) && tr.position == pos)
        {
            pos += Vector3.right;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.A) && tr.position == pos)
        {
            pos += Vector3.left;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.W) && tr.position == pos)
        {
            pos += Vector3.up;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.S) && tr.position == pos)
        {
            if (RaycastCheckUpdate(pos + Vector3.down))
            {
                
            pos += Vector3.down;
                stepSound.Play();
            }
        }
        
        //rb.velocity = Vector3.ClampMagnitude(pos, 1f) * speed;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    public RaycastHit2D CheckRaycast(Vector2 direction)
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        return Physics2D.Raycast(startingPosition, direction, raycastMaxDistance, 8);
    }

    private bool RaycastCheckUpdate(Vector3 dir)
    {
            Debug.DrawRay(transform.position, dir, Color.green, 1f);
        
            Vector2 direction = dir;

            RaycastHit2D hit = CheckRaycast(direction);

            if (hit.collider)
            {
                Debug.Log("hit "+ hit.collider.name);
                return true;
            }
            return false;
        
    }

    private bool WallCheck(Vector3 dir)
    {
        if (RaycastCheckUpdate(dir))
        {
            return true;
        }
        stepSound.Play();
        return false;
    }
}
