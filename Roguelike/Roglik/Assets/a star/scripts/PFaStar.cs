using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFaStar : MonoBehaviour
{
    private static List<PFNode> _completePath;
    private static PFgrid _grid;
    
    private static readonly List<PFNode> _openSet = new List<PFNode>();
    private static readonly List<PFNode> _closedSet = new List<PFNode>();

    private void Awake()
    {
        _grid = GetComponent<PFgrid>();
    }

    public static List<PFNode> FindPath(Vector3 startingCell, Vector3 targetCell)
    {
		bool isDestinationUnwalkable = false; // Testing system for NPC collision
		
        _openSet.Clear();
        _closedSet.Clear();

        //assign start and goal
        var startNode = PFgrid.NodeFromWorldPoint(startingCell);
        var targetNode = PFgrid.NodeFromWorldPoint(targetCell);

		if(!targetNode.Walkable)
		{
			isDestinationUnwalkable = true;
			targetNode.Walkable = true;//trick
		}

        _openSet.Add(startNode);

        while (_openSet.Count > 0)
        {
            var winnerNode = _openSet[0];

            foreach (var node in _openSet)
            {
                if (node.FCost > winnerNode.FCost)
                    continue;
                
                if (node.HCost < winnerNode.HCost)
                {
                    winnerNode = node;
                }
            }

            _openSet.Remove(winnerNode);
            _closedSet.Add(winnerNode);

            if(winnerNode == targetNode) // DONE!
            {
                RetracePath(startNode, targetNode);
				if(isDestinationUnwalkable)
				{
					targetNode.Walkable = false;
					isDestinationUnwalkable = false;
				}
                return _completePath;
            }

            foreach(var neighbour in _grid.GetNeighbours(winnerNode))
            {
                if (!neighbour.Walkable || _closedSet.Contains(neighbour)) // if node is walkable and I didnt checked it before
                {
                    continue;
                }

                int newCostToNeighbour = winnerNode.GCost + 1; //or winnerNode.gCost +1; GetDistance(winnerNode, neighbour)

                if (newCostToNeighbour < neighbour.GCost || !_openSet.Contains(neighbour)) // if cost of neighbour is higher than new cost or I didnt checked it before add this new cost to it
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.cameFrom = winnerNode;

                    if (!_openSet.Contains(neighbour))
                        _openSet.Add(neighbour);
                }

            }
        }

		if(isDestinationUnwalkable)
		{
			targetNode.Walkable = false;//trick
			isDestinationUnwalkable = false;
		}
        return null;
    }

    private static int GetDistance(PFNode nodeA, PFNode nodeB) //heuristic guess
    {
        var dstX = Mathf.Abs(nodeA.x - nodeB.x);
        
        var dstY = Mathf.Abs(nodeA.y - nodeB.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private static void RetracePath(PFNode startNode, PFNode endNode)
    {
        var path = new List<PFNode>();
        var currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.cameFrom;
        }
        path.Reverse();

        _completePath = path;
    }
}
