using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    public Transform seeker, target;
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startnode = grid.NodeFromWorldPoint(startPos);
        Node targetnode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startnode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; ++i)
            {
                // This is the slowest part of the algorithm
                // We are setting the currentNode to the node in the openSet with the lowest fCost.
                // If the fCost is the same, we check the hCost (Heuristic or 'predicted' cost) and set it to the node that is nearer to the end
                // This is terribly unoptimized
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetnode)
            {
                RetracePath(startnode, targetnode);
                return;
            }

            foreach (Node neighbor in grid.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;
                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetnode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }

    private int GetDistance(Node a, Node b)
    {
        int xDist = Mathf.Abs(a.gridX - b.gridX);
        int yDist = Mathf.Abs(a.gridY - b.gridY);

        return (xDist > yDist) ? (14 * yDist + 10 * (xDist - yDist)) : (14 * xDist + 10 * (yDist - xDist));
    }

    private void RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentnode = end;
        while (currentnode != start)
        {
            path.Add(currentnode);
            currentnode = currentnode.parent;
        }
        path.Reverse();
        grid.path = path;
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }
}
