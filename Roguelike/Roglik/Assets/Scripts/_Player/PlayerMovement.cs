using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public AudioSource stepSound;
    public float speed = 3.5f;
    public LayerMask wallLayer;

    private const float RAYCAST_DISTANCE = 1f; // One cell
    private Vector3 pos;
    //private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        pos = transform.position;
        //rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D) && transform.position == pos && !RayWallUpdate(Vector3.right))
        {
            pos += Vector3.right;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.A) && transform.position == pos && !RayWallUpdate(Vector3.left))
        {
            pos += Vector3.left;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == pos && !RayWallUpdate(Vector3.up))
        {
            pos += Vector3.up;
            stepSound.Play();
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == pos && !RayWallUpdate(Vector3.down))
        {
            pos += Vector3.down;
            stepSound.Play();
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }


    //Raycast for wall collision
    bool RayWallUpdate(Vector2 rayDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, RAYCAST_DISTANCE, wallLayer);
        Debug.DrawRay(transform.position, rayDirection, Color.green, RAYCAST_DISTANCE);
        
        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }
}
