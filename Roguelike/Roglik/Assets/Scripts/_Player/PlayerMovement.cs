using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public AudioSource stepSound;
    public float speed = 3.5f;
    public LayerMask wallLayer;


    private const float RAYCAST_DISTANCE = 1f; // One cell
    private static Vector3 pos;
    private static LayerMask statWallLayer;

    public static Vector3 PlayerPos3 { get { return pos; } }
    public static Vector2Int PlayerPos2 { get { return new Vector2Int((int)pos.x, (int)pos.y); } }

    void Start () {
        pos = transform.position;
        statWallLayer = wallLayer;
    }
	
	void Update () {

        if (Input.GetKey(KeyCode.D) && transform.position == pos && !RayWallUpdate(Vector3.right, RAYCAST_DISTANCE))
        {
            pos += Vector3.right;
            stepSound.Play();
            TurnSystem.enemyTurn();
        }
        else if (Input.GetKey(KeyCode.A) && transform.position == pos && !RayWallUpdate(Vector3.left, RAYCAST_DISTANCE))
        {
            pos += Vector3.left;
            stepSound.Play();
            TurnSystem.enemyTurn();
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == pos && !RayWallUpdate(Vector3.up, RAYCAST_DISTANCE))
        {
            pos += Vector3.up;
            stepSound.Play();
            TurnSystem.enemyTurn();
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == pos && !RayWallUpdate(Vector3.down, RAYCAST_DISTANCE))
        {
            pos += Vector3.down;
            stepSound.Play();
            TurnSystem.enemyTurn();
        }
        //delegate to npc's to raycast player in order to reduce thier update function weight 

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }


    //Raycast for wall collision
    public static bool RayWallUpdate(Vector3 rayDirection, float raycastDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, rayDirection, raycastDistance, statWallLayer);
        Debug.DrawRay(pos, rayDirection, Color.green, 1f);
        
        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }
}
