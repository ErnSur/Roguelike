using System.Collections.Generic;
using System.Linq;
using LDF.Systems.Pathfinding;
using LDF.Utils;
using UnityEngine;
using static LDF.Systems.Pathfinding.GlobalFunctions;

public class ScenePathfinding : MonoBehaviour
{
    private static PathfindingGrid _currentLevelGrid;
    private static Transform _currentLevelGridTransform;
    private static Vector2Int _currentCellSize;
    
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
        return _currentLevelGrid.FindPath(GetNodeFromWorldPosition(from), GetNodeFromWorldPosition(to));
    }
    
    public static IEnumerable<Vector3> GetPositionPath(Vector3 from, Vector3 to)
    {
        return _currentLevelGrid.FindPath(GetNodeFromWorldPosition(from), GetNodeFromWorldPosition(to)).Select(GetNodeWorldPosition);
    }

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

    private static Vector3 GetNodeWorldPosition(Node node)
    {
        return node.Pos.ToVector3() + _currentLevelGridTransform.position + _currentCellSize.ToVector3() / 2;
    }
#if !DEBUG
    private void OnDrawGizmos()
    {
        for (int x = 0; x < _grid.LengthX; x++)
        {
            for (int y = 0; y < _grid.LengthY; y++)
            {
                Gizmos.color = ColorExtensions.GetRandomColor(x + y * _grid.LengthX);
                if (_grid[x, y].Walkable)
                    Gizmos.DrawCube(GetNodeWorldPosition(_grid[x, y]), new Vector3(1, 1, 0));
            }
        }
    }

#endif
}