using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LDF.Systems.Pathfinding;
using LDF.Utility;
using UnityEngine;
using static LDF.Systems.Pathfinding.DefaultImplementations;

public class ScenePathfinding : MonoBehaviour
{
    private static PathfindingGrid _currentLevelGrid;
    private static Transform _currentLevelGridTransform;
    private static Vector2Int _currentCellSize = Vector2Int.one;
    
    [SerializeField]
    private PathfindingGrid _grid;

    [SerializeField]
    private Vector2Int _gridSize, _cellSize;

    [SerializeField]
    private LayerMask _wallLayermask;

    private void Awake()
    {
        CreatePathfindingGrid();
        _currentLevelGridTransform = transform;
        _currentCellSize = _cellSize;
    }

    public static bool IsWalkable(Vector2Int pos)
    {
        return pos.IsPositive() && _currentLevelGrid[pos].Walkable;
    }

    public static IEnumerable<Node> GetPath(Vector3 from, Vector3 to)
    {
        return _currentLevelGrid.FindPathAStar(GetNodeFromWorldPosition(from), GetNodeFromWorldPosition(to));
    }
    
    public static IEnumerable<Vector3> GetPositionPath(Vector3 from, Vector3 to)
    {
        return _currentLevelGrid.FindPathAStar(GetNodeFromWorldPosition(from), GetNodeFromWorldPosition(to)).Select(GetNodeWorldPosition);
    }

    [ContextMenu("Create Grid")]
    private void CreatePathfindingGrid()
    {
        var isNodeWalkableCallback = IsCellWalkable(_cellSize, _wallLayermask, transform.position);
        _grid = new PathfindingGrid(_gridSize.x, _gridSize.y, isNodeWalkableCallback);
        _currentLevelGrid = _grid;
    }

    private static Node GetNodeFromWorldPosition(Vector3 position)
    {
        var index = position - _currentLevelGridTransform.position;
        return _currentLevelGrid[(int) index.x, (int) index.y];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector3 GetNodeWorldPosition(Node node)
    {
        return GetNodeWorldPosition(node,_currentLevelGridTransform.position,_currentCellSize.ToVector3());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector3 GetNodeWorldPosition(Node node, Vector3 gridWorldPos, Vector3 cellSize)
    {
        return node.Pos.ToVector3() + gridWorldPos + cellSize / 2;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        _grid?.OnDrawGizmos(Vector3.one, GetNodePos);

        Vector3 GetNodePos(Node n)
        {
            return GetNodeWorldPosition(n, transform.position, Vector3.one);
        }
    }
#endif
}