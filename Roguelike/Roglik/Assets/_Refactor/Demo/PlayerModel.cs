using System.Collections.Generic;
using System.Linq;
using LDF.Systems;
using LDF.Systems.Pathfinding;
using LDF.Utils;
using UnityEngine;

public class PlayerModel : SystemBehaviour
{
    public Transform target;
    private List<Vector3> _path;
    
    
    private void OnGUI()
    {
        if (GUILayout.Button("Find Path To Target"))
        {
            _path = ScenePathfinding.GetPositionPath(transform.position, target.position).ToList();
            for (var index = 0; index < _path.Count; index++)
            {
                Debug.Log($"STEP [{index}]: {_path[index]}");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(_path == null)
            return;

        Gizmos.DrawLine(transform.position,_path[0]);
        
        for (var index = 1; index < _path.Count; index++)
        {
            Gizmos.DrawLine(_path[index-1],_path[index]);
        }
    }
}