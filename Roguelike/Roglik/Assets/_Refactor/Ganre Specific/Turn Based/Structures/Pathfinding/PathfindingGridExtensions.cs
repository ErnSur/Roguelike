using System.Collections.Generic;
using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    public static class PathfindingGridExtensions
    {
        private static AStar _aStar;

        public static List<Node> FindPath(this PathfindingGrid grid, Node startingCell, Node targetCell)
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
    }
}