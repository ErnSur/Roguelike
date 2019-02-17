using System.Collections.Generic;

namespace LDF.Systems.Pathfinding
{
    public class AStar
    {
        public delegate int GetHCost(Node nodeA, Node nodeB);

        private readonly List<Node> _openSet = new List<Node>();
        private readonly List<Node> _closedSet = new List<Node>();

        private readonly Node _startingNode;
        private readonly Node _targetNode;
        private readonly GetHCost _getHCost;

        private readonly PathfindingGrid _grid;

        private readonly bool _targetNodeWasUnwalkable;

        public AStar(PathfindingGrid grid, Node startingNode, Node targetNode, GetHCost getHCost)
        {
            _grid = grid;
            _startingNode = startingNode;
            _targetNode = targetNode;
            _getHCost = getHCost;
            _targetNodeWasUnwalkable = !_targetNode.Walkable;
            _targetNode.Walkable = true;

            _openSet.Add(_startingNode);
        }

        public List<Node> GetPath()
        {
            FindAPathFromOpenSet();

            UpdateTargetNodeToOriginalState();

            return RetracePath();
        }

        private void FindAPathFromOpenSet()
        {
            while (_openSet.Count > 0)
            {
                var winnerNode = _openSet[0];

                winnerNode = GetALowestCostNode(winnerNode);

                _openSet.Remove(winnerNode);
                _closedSet.Add(winnerNode);

                if (winnerNode == _targetNode) // DONE!
                {
                    return;
                }

                CalculatePriceOfWinnerNodeNeighbours(winnerNode);
            }
        }

        private void CalculatePriceOfWinnerNodeNeighbours(Node winnerNode)
        {
            foreach (var neighbour in _grid.GetNeighbours(winnerNode))
            {
                if (!neighbour.Walkable || _closedSet.Contains(neighbour)
                ) // if node is walkable and I didnt checked it before
                {
                    continue;
                }

                var newNeighbourCost = winnerNode.GCost + 1;

                if (newNeighbourCost < neighbour.GCost || !_openSet.Contains(neighbour)
                ) // if cost of neighbour is higher than new cost or I didnt checked it before add this new cost to it
                {
                    neighbour.GCost = newNeighbourCost;
                    neighbour.HCost = _getHCost(neighbour, _targetNode);
                    neighbour.CameFrom = winnerNode;

                    if (!_openSet.Contains(neighbour))
                        _openSet.Add(neighbour);
                }
            }
        }

        private Node GetALowestCostNode(Node winnerNode)
        {
            foreach (var node in _openSet)
            {
                if (node.FCost > winnerNode.FCost)
                    continue;

                if (node.HCost < winnerNode.HCost)
                {
                    winnerNode = node;
                }
            }

            return winnerNode;
        }

        private void UpdateTargetNodeToOriginalState()
        {
            if (_targetNodeWasUnwalkable)
            {
                _targetNode.Walkable = false;
            }
        }

        private List<Node> RetracePath()
        {
            if (_targetNode.CameFrom == null)
                return null;
            
            var path = new List<Node>();
            var currentNode = _targetNode;

            while (currentNode != _startingNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.CameFrom;
            }

            path.Reverse();

            return path;
        }
    }
}