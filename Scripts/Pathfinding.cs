using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

	public Transform from;
	public Transform to;

	NodeGrid grid;

	void Awake()
	{
		grid = GetComponent<NodeGrid>();
	}

	void Update()
	{
		grid.path = FindPath(from.position, to.position);
		print(grid.path.Count);
	}

	public List<Node> FindPath(Vector3 beginPos, Vector3 targetPos)
	{
		Node startNode = grid.GetNodeFromWorldPoint(beginPos);
		Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

		return AStar(startNode, targetNode);
	}

	List<Node> AStar(Node start, Node end)
	{
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			Node currentNode = openSet[0];

			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
				{
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == end)
			{
				return RetracePath (start, end);
			}

			foreach (Node neighbour in grid.GetNeighboursOfNode(currentNode))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
					continue;

				if (!openSet.Contains(neighbour))
				{
					neighbour.hCost = grid.GetDistanceBetweenNodes(neighbour, end);
					openSet.Add(neighbour);
				}

				int costToNeighbour = currentNode.gCost + grid.GetDistanceBetweenNodes(currentNode, neighbour);

				if (costToNeighbour < neighbour.gCost)
				{
					neighbour.gCost = costToNeighbour;
					neighbour.parent = currentNode;
				}
				
			}
		}

		return null;
	}

	List<Node> RetracePath(Node start, Node end)
	{
		List<Node> path = new List<Node>();
		Node current = end;

		while (current != start)
		{
			path.Add(current);
			current = current.parent;
		}
		path.Reverse();
		return path;
	}

}
