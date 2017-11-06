using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Sprite[] directionSprites; //0up,1down,2left,3right
	public SpriteRenderer spriteRender;

    public float speed = 3.5f;
    public LayerMask wallLayer;

	private PlayerStats stats;
    private const float RAYCAST_DISTANCE = 1f; // One cell
    private static LayerMask statWallLayer;
    private AudioSource audioSrc;
    public AudioClip[] stepSound1;
    public AudioClip[] stepSound2;
    public AudioClip stepSoundOrginal;
	[Range(0,1)]public float orginalStepVolume;
	int foot = 1;


    void Start () {
        statWallLayer = wallLayer;
        audioSrc = GetComponent<AudioSource>();
		spriteRender = GetComponent<SpriteRenderer>();
		stats = PlayerStats.instance;
    }

	void Update () {

        if (Input.GetKey(KeyCode.D) && transform.position == PlayerStats.instance.Position)
        {
			spriteRender.sprite = directionSprites[3];
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x+1,(int)PlayerStats.instance.Position.y].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.right;
				StartCoroutine("FootstepSound");
				//StartCoroutine("MoveToPosition");
				SkipTurn();
			}
        }
        else if (Input.GetKey(KeyCode.A) && transform.position == PlayerStats.instance.Position)
        {
			spriteRender.sprite = directionSprites[2];
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x-1,(int)PlayerStats.instance.Position.y].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.left;
				StartCoroutine("FootstepSound");
				//StartCoroutine("MoveToPosition");
				SkipTurn();
			}
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == PlayerStats.instance.Position)
        {
			spriteRender.sprite = directionSprites[0];
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x,(int)PlayerStats.instance.Position.y+1].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.up;
				StartCoroutine("FootstepSound");
				//StartCoroutine("MoveToPosition");
				SkipTurn();
			}
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == PlayerStats.instance.Position)
        {
			spriteRender.sprite = directionSprites[1];
			if(PFgrid.grid[(int)PlayerStats.instance.Position.x,(int)PlayerStats.instance.Position.y-1].walkable == true)
			{
				PlayerStats.instance.Position += Vector3.down;
				StartCoroutine("FootstepSound");
				//StartCoroutine("MoveToPosition");
				SkipTurn();
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
		if (TurnSystem.nextTurn != null)
			TurnSystem.nextTurn();
	}

	IEnumerator FootstepSound()
    {
		if (foot == 1)
		{
			audioSrc.PlayOneShot(stepSound1[Random.Range(0,stepSound1.Length)]);
			audioSrc.PlayOneShot(stepSoundOrginal, orginalStepVolume);
			//audioSrc.clip = stepSound1[Random.Range(0,stepSound1.Length)]; //maybe do it on corutine or dont destroy object, just change its sprite and disable state
			foot = 2;
		} else
		{
			audioSrc.PlayOneShot(stepSound2[Random.Range(0,stepSound2.Length)]);
			audioSrc.PlayOneShot(stepSoundOrginal, orginalStepVolume);
			//audioSrc.clip = stepSound2[Random.Range(0,stepSound2.Length)]; //maybe do it on corutine or dont destroy object, just change its sprite and disable state
			foot = 1;
		}
		yield return null;
    }

	IEnumerator MoveToPosition()
    {
        while (transform.position != PlayerStats.instance.Position)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerStats.instance.Position, Time.deltaTime * speed);
            yield return null;
        }
		//Debug.Log("wewe");
		SkipTurn();
    }
}
