using System.Collections.Generic;
using LDF.Structures;
using LDF.Utils;
using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    [System.Serializable]
    public class PathfindingGrid
    {
        [field: SerializeField]
        public int LengthX { get; private set; }

        [field: SerializeField]
        public int LengthY { get; private set; }

        [SerializeField]
        private Node[] _grid;

        private readonly IsCellWalkable _isCellWalkable;

        private readonly Vector2Int[] _directions = DirectionExtensions.Get4Directions().AsVector2Int();

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

        public PathfindingGrid(int lengthX, int lengthY, IsCellWalkable isCellWalkable)
        {
            this.LengthX = lengthX;
            this.LengthY = lengthY;
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

        private bool IsPositionContainedInGrid(Vector2Int pos)
        {
            return (pos.x >= 0 && pos.x < LengthX) && (pos.y >= 0 && pos.y < LengthY);
        }

        public IEnumerable<Node> GetNeighbours(Node node)
        {
            var neighbours = new HashSet<Node>();

            foreach (var direction in _directions)
            {
                var neighbourPos = node.Pos + direction;

                if (IsPositionContainedInGrid(neighbourPos))
                {
                    neighbours.Add(this[neighbourPos.x, neighbourPos.y]);
                }
            }

            return neighbours;
        }

        public delegate bool IsCellWalkable(int x, int y);
    }
}