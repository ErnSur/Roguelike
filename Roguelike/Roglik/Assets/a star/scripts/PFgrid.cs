using System.Collections.Generic;
using UnityEngine;

public class PFgrid : MonoBehaviour
{   
    public static PFNode[,] grid;

    public LayerMask wallLayermask;

    public int gridSizeX;
    public int gridSizeY;

    private readonly Vector2 _gridCellSizeInUnits = new Vector2(0.5f, 0.5f);
    
    private static Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        
        CreateNewGrid();
        
        FillGrid();
    }

    private void FillGrid()
    {
        for (var x = 0; x < gridSizeX; x++)
        {
            for (var y = 0; y < gridSizeY; y++)
            {
                if (Physics2D.OverlapBox(new Vector2(x, y), _gridCellSizeInUnits, 0, wallLayermask))
                {
                    grid[x, y] = new PFNode(x, y, false);
                }
                else
                {
                    grid[x, y] = new PFNode(x, y, true);
                }
            }
        }
    }

    private void CreateNewGrid()
    {
        grid = new PFNode[gridSizeX + 1, gridSizeY + 1];
    }

    public List<PFNode> GetNeighbours(PFNode node)
    {
        var neighbours = new List<PFNode>();

        for (var x = -1; x <= 1; x++) // TO REWORK
        {
            for (var y = -1; y <= 1; y++)
            {
                switch (x)
                {
                    case 0 when y == 0:
                    case -1 when y == 1:
                    case 1 when y == 1:
                    case -1 when y == -1:
                    case 1 when y == -1:
                        continue;
                }

                var checkX = node.x + x;
                var checkY = node.y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    
    public static PFNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        return grid[(int) worldPosition.x, (int) worldPosition.y];
    }

    //new functions 1. screen to grid, world to grid, vector3 -> vector2
    public static Vector3 ScreenToGridCell (Vector3 screenPointerPos)
    {
        screenPointerPos = _cam.ScreenToWorldPoint(screenPointerPos);
        screenPointerPos = Vector3Int.RoundToInt(screenPointerPos);

        Vector3 gridPos = new Vector3( (int)screenPointerPos.x, (int)screenPointerPos.y, 0f);
        return gridPos;
    }

    public static Vector2Int WorldToGridCell(Vector3 worldPos)
    {
        worldPos = Vector3Int.RoundToInt(worldPos);
        Vector2Int gridPos = new Vector2Int((int)worldPos.x, (int)worldPos.y);

        return gridPos;
    }
}
