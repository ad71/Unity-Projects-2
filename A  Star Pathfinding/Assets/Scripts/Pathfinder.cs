using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

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
                // We aer setting the currentNode to the node in the openSet with the lowest fCost.
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
                return;
            }


        }
    }
}
