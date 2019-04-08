using UnityEngine;

[CreateAssetMenu(menuName = "Trigger/On Player Sight")]
public class TriggerPlayerSight : Trigger
{
    public LayerMask playerAndWall;
    public float noticeRange;

    public override bool TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone)
    {
        if (!PlayerTorch.torch)
        {
            return false;
        }
        Vector3 dir = target - watcher;
        //if((dir.x * dir.x)+(dir.y * dir.y) <= 36)
        //{
        RaycastHit2D hit = Physics2D.Raycast(watcher, dir, noticeRange, playerAndWall);
        Debug.DrawRay(watcher, dir, Color.green, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Debug.Log("saw " + hit.collider.name);
            FearSystem.IncreaseFear(10);
            return true;
        }
        //}
        return false;
    }
}