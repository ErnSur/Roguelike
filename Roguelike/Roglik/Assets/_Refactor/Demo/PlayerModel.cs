using System.Collections.Generic;
using System.Linq;
using LDF.Structures;
using LDF.Systems;
using UnityEngine;

public class PlayerModel : SystemBehaviour, IPlayerMovement_SInput
{
    private GridTransform_S _gridTransform;
    public Transform target;

    public float PlayerSpeed { get; } = 2;

    public Vector2Int PlayerGridPos { get; set; }
    
    private List<Vector3> _path = new List<Vector3>();
    public Transform PlayerTransform => transform;

    public bool CanMoveInDirection(Vector2Int direction)
    {
        return ScenePathfinding.IsWalkable(PlayerGridPos + direction);
    }

    protected override void Init()
    {
       PlayerGridPos = this.Lazy(ref _gridTransform).Position;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Path To Target"))
        {
            _path = ScenePathfinding.GetPositionPath(transform.position, target.position).ToList();
        }
    }

    private void OnDrawGizmos()
    {
        if(_path?.Count <= 0 || _path == null)
            return;

        Gizmos.DrawLine(transform.position,_path[0]);
        
        for (var index = 1; index < _path.Count; index++)
        {
            Gizmos.DrawLine(_path[index-1],_path[index]);
        }
    }
}