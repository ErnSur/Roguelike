using System.Collections.Generic;
using System.Linq;
using LDF.Systems;
using UnityEngine;
using LDF.Utility;

public class PlayerModel : SystemBehaviour, IPlayerMovement_SInput
{
    public Transform target;

    [field: SerializeField, NameFix]
    public float PlayerSpeed { get; private set; } = 2;
    
    [field: SerializeField, NameFix]
    public Vector2Int PlayerGridPos { get; set; }
    
    private List<Vector3> _path = new List<Vector3>();
    public Transform PlayerTransform => transform;

    public bool CanMoveInDirection(Vector2Int direction)
    {
        return ScenePathfinding.IsWalkable(PlayerGridPos + direction);
    }

    protected override void Init()
    {
       PlayerGridPos = transform.GridPosition();
    }

    #if DEBUG
    private void OnGUI()
    {
        if (GUILayout.Button("Find Path To Target"))
        {
            _path = ScenePathfinding.GetPositionPath(transform.position, target.position).ToList();
        }
    }

    private void OnValidate()
    {
        PlayerGridPos = PlayerTransform.GridPosition();
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
    #endif
}