using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    public static partial class GlobalFunctions
    {
        private static PathfindingGrid.IsCellWalkable _isCellWalkable;
        
        public static PathfindingGrid.IsCellWalkable Default(this PathfindingGrid.IsCellWalkable f,
        Vector2 cellSize, LayerMask wallLayermask, Vector2 offSet)
        {
            return DefaultImplementation;
            
            bool DefaultImplementation(int x, int y)
            {
                var rayCastSize = cellSize / 2;
                var rayCenter = new Vector2(x + offSet.x + rayCastSize.x, y + offSet.y + rayCastSize.y);
                
                return !Physics2D.OverlapBox(rayCenter, rayCastSize, 0, wallLayermask);
            }
        }
        
        public static PathfindingGrid.IsCellWalkable IsCellWalkable(Vector2 cellSize, LayerMask wallLayermask, Vector2 offSet)
            => _isCellWalkable.Default(cellSize, wallLayermask, offSet);

        
    }
}