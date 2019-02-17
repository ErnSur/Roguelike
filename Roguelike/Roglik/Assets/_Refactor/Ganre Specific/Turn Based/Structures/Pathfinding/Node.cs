using UnityEngine;

namespace LDF.Systems.Pathfinding
{
    [System.Serializable]
    public class Node
    {
        [field: SerializeField]
        public int X { get; private set; }

        [field: SerializeField]
        public int Y { get; private set; }

        public Vector2Int Pos => new Vector2Int(X, Y);

        internal int GCost { get; set; }
        internal int HCost { get; set; }
        internal int FCost => GCost + HCost;

        [field: SerializeField]
        public bool Walkable { get; internal set; }

        internal Node CameFrom { get; set; }

        public Node(int x, int y, bool walkable)
        {
            Walkable = walkable;
            this.X = x;
            this.Y = y;
        }
        
        public static explicit operator (int x, int y)(Node n)  // explicit byte to digit conversion operator
        {
            return (n.X, n.Y);
        }
    }
}