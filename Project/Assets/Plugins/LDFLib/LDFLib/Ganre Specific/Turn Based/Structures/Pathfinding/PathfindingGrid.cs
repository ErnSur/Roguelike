using System.Collections.Generic;
using LDF.Structures;
using LDF.Utility;
using UnityEngine;
using System.Linq;

namespace LDF.Systems.Pathfinding
{
    [System.Serializable]
    public class PathfindingGrid
    {
        [field: SerializeField]
        public int LengthX { get; private set; }

        [field: SerializeField]
        public int LengthY { get; private set; }

        [SerializeField, ReadOnly]
        private Node[] _grid;

        private readonly IsCellWalkable _isCellWalkable;

        private static readonly Vector2Int[] _directions = DirectionExtensions.Get4Directions().AsVector2Int();

        public PathfindingGrid(int lengthX, int lengthY, IsCellWalkable isCellWalkable)
        {
            LengthX = lengthX;
            LengthY = lengthY;
            _isCellWalkable = isCellWalkable;

            _grid = new Node[lengthX * lengthY];

            FillGrid();
        }

        private void FillGrid()
        {
            for (var x = 0; x < LengthX; x++)
            {
                for (var y = 0; y < LengthY; y++)
                {
                    this[x, y] = new Node(x, y, _isCellWalkable(x, y));
                }
            }
        }

        private bool Contains(Vector2Int coordinate)
        {
            return (coordinate.x >= 0 && coordinate.x < LengthX)
                && (coordinate.y >= 0 && coordinate.y < LengthY);
        }

        public IEnumerable<Node> GetNeighbours(Node node)
        {
            return from direction in _directions
                   let neighbourPos = node.Pos + direction
                   where Contains(neighbourPos)
                   select this[neighbourPos];
        }
        
        public Node this[int x, int y]
        {
            get => _grid.Get2D(x, y, LengthX);
            private set => _grid.Set2D(x, y, LengthX, value);
        }
        
        public Node this[Vector2Int pos]
        {
            get => _grid.Get2D(pos.x, pos.y, LengthX);
            private set => _grid.Set2D(pos.x, pos.y, LengthX, value);
        }

        public delegate bool IsCellWalkable(int x, int y);
    }
}