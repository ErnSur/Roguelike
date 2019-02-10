using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    public static partial class GlobalFunctions
    {
        private static PathfindingGrid.IsCellWalkable _isCellWalkable;
        
        public static PathfindingGrid.IsCellWalkable Default(this PathfindingGrid.IsCellWalkable f, Vector2 raycastBoxSize, LayerMask wallLayermask)
        {
            return DefaultImplementation;
            
            bool DefaultImplementation(int x, int y)
            {
                return Physics2D.OverlapBox(new Vector2(x, y), raycastBoxSize, 0, wallLayermask);
            }
        }
        
        public static PathfindingGrid.IsCellWalkable IsCellWalkable(Vector2 raycastBoxSize, LayerMask wallLayermask)
            => _isCellWalkable.Default(raycastBoxSize, wallLayermask);
    }
}