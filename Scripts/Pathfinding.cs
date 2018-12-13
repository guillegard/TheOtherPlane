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
	}

	public List<Node> FindPath(Vector3 beginPos, Vector3 targetPos)
	{
		Node startNode = grid.GetNodeFromWorldPoint(beginPos);
		Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

		return AStar(startNode, targetNode);
	}

	List<Node> AStar(Node start, Node end)
	{
		Heap<Node> openSet = new Heap<Node>(grid.NodesInGrid);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			Node currentNode = openSet.RemoveFirst();

			closedSet.Add(currentNode);

			if (currentNode == end)
			{
				return RetracePath (start, end);
			}

			foreach (Node neighbour in grid.GetNeighboursOfNode(currentNode))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
					continue;

				int costToNeighbour = currentNode.gCost + grid.GetDistanceBetweenNodes(currentNode, neighbour);

				if (costToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = costToNeighbour;
					neighbour.hCost = grid.GetDistanceBetweenNodes(neighbour, end);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);

					openSet.UpdateItem(neighbour);
					
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
