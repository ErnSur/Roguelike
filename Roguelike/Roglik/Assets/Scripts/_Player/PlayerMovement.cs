using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float speed = 3.5f;
    public LayerMask wallLayer;


    private const float RAYCAST_DISTANCE = 1f; // One cell
    private static LayerMask statWallLayer;
    private AudioSource audioSrc;
    public AudioClip stepSound;


    void Start () {
        statWallLayer = wallLayer;
        audioSrc = GetComponent<AudioSource>();
    }

	void Update () {

        if (Input.GetKey(KeyCode.D) && transform.position == PlayerStats.instance.Position)
        {
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x+1,(int)PlayerStats.instance.Position.y].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.right;
				audioSrc.clip = stepSound;
				audioSrc.Play();
				TurnSystem.nextTurn();
			}
        }
        else if (Input.GetKey(KeyCode.A) && transform.position == PlayerStats.instance.Position)
        {
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x-1,(int)PlayerStats.instance.Position.y].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.left;
				audioSrc.clip = stepSound;
				audioSrc.Play();
				TurnSystem.nextTurn();
			}
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == PlayerStats.instance.Position)
        {
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x,(int)PlayerStats.instance.Position.y+1].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.up;
				audioSrc.clip = stepSound;
				audioSrc.Play();
				TurnSystem.nextTurn();
			}
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == PlayerStats.instance.Position)
        {
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x,(int)PlayerStats.instance.Position.y-1].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.down;
				audioSrc.clip = stepSound;
				audioSrc.Play();
				TurnSystem.nextTurn();
			}
        }else if (Input.GetButtonDown("Cancel"))
		{
			SkipTurn();
		}

        transform.position = Vector3.MoveTowards(transform.position, PlayerStats.instance.Position, Time.deltaTime * speed);
        //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }


    //Raycast for wall collision
    public static bool RayWallUpdate(Vector3 rayDirection, float raycastDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerStats.instance.Position, rayDirection, raycastDistance, statWallLayer);
        Debug.DrawRay(PlayerStats.instance.Position, rayDirection, Color.green, 1f);

        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }

	public void SkipTurn()
	{
		TurnSystem.nextTurn();
	}
}
