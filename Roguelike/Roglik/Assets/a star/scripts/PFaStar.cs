﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFaStar : MonoBehaviour {

    PFgrid grid;

    public List<PFnode> completePath;

    private void Awake()
    {
        grid = GetComponent<PFgrid>();
    }

    public List<PFnode> FindPath(Transform startingCell, Transform targetCell)
    {

        List<PFnode> openSet = new List<PFnode>();
        List<PFnode> closedSet = new List<PFnode>();

        //assign start and goal
        PFnode startNode = grid.NodeFromWorldPoint(startingCell);
        PFnode targetNode = grid.NodeFromWorldPoint(targetCell);

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            PFnode winnerNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost <= winnerNode.fCost)
                {
                    if (openSet[i].hCost < winnerNode.hCost)
                    {
                        winnerNode = openSet[i];
                    }
                }
            }

            openSet.Remove(winnerNode);
            closedSet.Add(winnerNode);

            if(winnerNode == targetNode) // DONE!
            {
                print("found path");
                RetracePath(startNode, targetNode);
                return completePath;
            }

            foreach(PFnode neighbour in grid.GetNeighbours(winnerNode)) //
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) // if node is walkable and I didnt checked it before
                {
                    continue;
                }

                int newCostToNeighbour = winnerNode.gCost + 1; //or winnerNode.gCost +1; GetDistance(winnerNode, neighbour)

                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) // if cost of neighbour is higher than new cost or I didnt checked it before add this new cost to it
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.cameFrom = winnerNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }

            }
        }

        return null;
    }

    int GetDistance(PFnode nodeA, PFnode nodeB) //heuristic guess
    {
        int dstX = Mathf.Abs((int)nodeA.x - (int)nodeB.x);
        int dstY = Mathf.Abs((int)nodeA.y - (int)nodeB.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    void RetracePath(PFnode startNode, PFnode endNode) //after geting to end node retrace a path that lead there
    {
        List<PFnode> path = new List<PFnode>();
        PFnode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.cameFrom;
        }
        path.Reverse();

        completePath = path;
        /*
        foreach (PFnode node in completePath)
        {
            Color color = Color.black;
            color.a = 0.4f;
            node.myCell.color = color;
        }
        */
    }
}