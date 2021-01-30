using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    public static class PathfindingGridExtensions
    {
        private static AStar _aStar;

        //todo: make this process async?
        public static List<Node> FindPathAStar(this PathfindingGrid grid, Node startingCell, Node targetCell)
        {
            _aStar = new AStar
                (
                    grid,
                    startingCell,
                    targetCell,
                    GetDistance
                );

            return _aStar.GetPath();

            int GetDistance(Node nodeA, Node nodeB)
            {
                var distanceOnXAxis = Mathf.Abs(nodeA.X - nodeB.X);

                var distanceOnYAxis = Mathf.Abs(nodeA.Y - nodeB.Y);

                if (distanceOnXAxis > distanceOnYAxis)
                    return 14 * distanceOnYAxis + 10 * (distanceOnXAxis - distanceOnYAxis);
                return 14 * distanceOnXAxis + 10 * (distanceOnYAxis - distanceOnXAxis);
            }
        }

        public static void OnDrawGizmos(this PathfindingGrid g, Vector3 cellSize, Func<Node, Vector3> getNodeWorldPos)
        {
            for (int x = 0; x < g.LengthX; x++)
            {
                for (int y = 0; y < g.LengthY; y++)
                {
                    Gizmos.color = g[x, y].Walkable ? new Color(0, 1, 0, 0.2f) : new Color(1, 0, 0, 0.2f);

                    Gizmos.DrawCube(getNodeWorldPos(g[x, y]), cellSize);
                }
            }
            Gizmos.color = Color.white;
        }
    }
}