using LDF.Systems.Pathfinding;
using LDF.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using static LDF.Systems.Pathfinding.GlobalFunctions;

public class ScenePathfinding : MonoBehaviour
{
    public static PathfindingGrid Grid { get; private set; }

    [SerializeField]
    private PathfindingGrid _grid;

    [SerializeField]
    private Vector2Int _gridSize, _cellSize;

    [SerializeField]
    private LayerMask _wallLayermask;

    private void Awake()
    {
        CreatePathfindingGrid();
    }

    private void CreatePathfindingGrid()
    {
        var isNodeWalkableCallback = IsCellWalkable(_cellSize, _wallLayermask, transform.position);
        _grid = new PathfindingGrid(_gridSize.x, _gridSize.y, isNodeWalkableCallback);
        Grid = _grid;
    }

    private Node GetNodeFromWorldPosition(Vector3 position)
    {
        var index = position - transform.position;
        return _grid[(int) index.x, (int) index.y];
    }

#if DEBUG
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

    private Vector3 GetNodeWorldPosition(Node node)
    {
        return node.Pos.ToVector3() + transform.position + _cellSize.ToVector3() / 2;
    }
#endif
}