using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour {

	NodeGrid grid;
	PathRequestManager requestManager;

	void Awake()
	{
		requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<NodeGrid>();
	}

	public void StartFindPath(Vector3 startPos, Vector3 endPos)
	{
		StartCoroutine(FindPath(startPos, endPos));
	}

	IEnumerator FindPath(Vector3 beginPos, Vector3 targetPos)
	{
		Node startNode = grid.GetNodeFromWorldPoint(beginPos);
		Node endNode = grid.GetNodeFromWorldPoint(targetPos);


		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		if (startNode.walkable && endNode.walkable && startNode != endNode)
		{

			Heap<Node> openSet = new Heap<Node>(grid.NodesInGrid);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				Node currentNode = openSet.RemoveFirst();

				closedSet.Add(currentNode);

				if (currentNode == endNode)
				{
					pathSuccess = true;
					break;
				}

				foreach (Node neighbour in grid.GetNeighboursOfNode(currentNode))
				{
					if (!neighbour.walkable || closedSet.Contains(neighbour))
						continue;

					int costToNeighbour = currentNode.gCost + grid.GetDistanceBetweenNodes(currentNode, neighbour);

					if (costToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = costToNeighbour;
						neighbour.hCost = grid.GetDistanceBetweenNodes(neighbour, endNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);

						openSet.UpdateItem(neighbour);

					}

				}
			}
		}
		yield return null;
		if (pathSuccess)
		{
			waypoints = RetracePath(startNode, endNode);
		}

		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	}

	Vector3[] RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;


		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path)
	{
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 0; i < path.Count; i++)
		{
			//Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
			//if (directionNew != directionOld)
				waypoints.Add(path[i].worldPos);
			//directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

}
