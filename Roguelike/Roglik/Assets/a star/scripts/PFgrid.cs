using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PFgrid : MonoBehaviour {

    public LayerMask wallLayermask;

    public int gridSizeX;
    public int gridSizeY;

    public static PFnode[,] grid;

#region Make Grid
    private void Awake()
    {
        grid = new PFnode[gridSizeX+1, gridSizeY+1];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                #region creater
                if(Physics2D.OverlapBox(new Vector2(x, y), new Vector2 (0.5f,0.5f), 0, wallLayermask) != null)
                {
                    grid[x, y] = new PFnode(x,y,false);
                }
                else
                {
                    grid[x, y] = new PFnode(x, y, true);
                }
                #endregion
            }
        }
    }

    public PFnode NodeFromWorldPoint(Vector3 objectsPosition)
    {
        int x = (int) objectsPosition.x;
        int y = (int) objectsPosition.y;

        return grid[x, y];
    }

    public List<PFnode> GetNeighbours(PFnode node)
    {
        List<PFnode> neighbours = new List<PFnode>();

        for (int x = -1; x <= 1; x++) // TO REWORK
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }else if (x == -1 && y == 1)
                {
                    continue;
                }
                else if (x == 1 && y == 1)
                {
                    continue;
                }
                else if (x == -1 && y == -1)
                {
                    continue;
                }
                else if (x == 1 && y == -1)
                {
                    continue;
                }

                int checkX = (int) node.x + x;
                int checkY = (int) node.y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
#endregion

    //new functions 1. screen to grid, world to grid, vector3 -> vector2
    public static Vector3 ScreenToGridCell (Vector3 ScreenPointerPos)
    {
        ScreenPointerPos = Camera.main.ScreenToWorldPoint(ScreenPointerPos);
        ScreenPointerPos = Vector3Int.RoundToInt(ScreenPointerPos);

        Vector3 gridPos = new Vector3( (int)ScreenPointerPos.x, (int)ScreenPointerPos.y, 0f);
        return gridPos;
    }

    public static Vector2Int WorldToGridCell(Vector3 worldPos)
    {
        worldPos = Vector3Int.RoundToInt(worldPos);
        Vector2Int gridPos = new Vector2Int((int)worldPos.x, (int)worldPos.y);

        return gridPos;
    }
}
