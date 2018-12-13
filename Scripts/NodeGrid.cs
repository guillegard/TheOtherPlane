﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {

	
	public List<Node> path;

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius = 1;

	Node[,] grid;
	int gridNodesX;
	int gridNodesY;
	float nodeDiameter;

	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		gridNodesX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridNodesY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();
	}

	void CreateGrid()
	{
		grid = new Node[gridNodesX, gridNodesY];
		Vector3 worldBotLeft = transform.position - Vector3.up * gridWorldSize.y / 2 - Vector3.right * gridWorldSize.x / 2;

		for (int x = 0; x < gridNodesX; x++)
		{
			for (int y = 0; y < gridNodesY; y++)
			{
				Vector3 worldPos = worldBotLeft + (Vector3.right * (x * nodeDiameter + nodeRadius)) + (Vector3.up * (y * nodeDiameter + nodeRadius));
				bool walkable = Physics2D.BoxCast(worldPos, new Vector2(nodeDiameter, nodeDiameter), 0, Vector2.zero, 0, unwalkableMask.value).collider == null;

				grid[x, y] = new Node(walkable, worldPos, x, y);
			}
		}
	}

	public List<Node> GetNeighboursOfNode(Node node)
	{
		List<Node> neighbours = new List<Node>();

		//Left neighbour
		if (node.gridX >= 1)
			neighbours.Add(grid[node.gridX - 1, node.gridY]);

		//right neighbour
		if (node.gridX < gridNodesX - 1)
			neighbours.Add(grid[node.gridX + 1, node.gridY]);

		//top neighbour
		if (node.gridY >= 1)
			neighbours.Add(grid[node.gridX, node.gridY - 1]);

		//down neighbour
		if (node.gridY < gridNodesY - 1)
			neighbours.Add(grid[node.gridX, node.gridY + 1]);

		return neighbours;
	}

	public int GetDistanceBetweenNodes(Node a, Node b)
	{
		int xDist = Mathf.Abs(a.gridX - b.gridX);
		int yDist = Mathf.Abs(a.gridY - b.gridY);

		return 10 * (yDist + xDist);
	}

	public Node GetNodeFromWorldPoint(Vector3 worldPos)
	{
		/**
		int x = Mathf.RoundToInt((worldPosition.x + gridWorldSize.x / 2 - nodeRadius)/nodeDiameter);
		int y = Mathf.RoundToInt((worldPosition.y + gridWorldSize.y / 2 - nodeRadius)/nodeDiameter);

		x = Mathf.Clamp(x, 0, gridSizeX - 1);
		y = Mathf.Clamp(y, 0, gridSizeY - 1);

		return grid[x, y];
		*/
		float percentX = Mathf.Clamp01((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
		float percentY = Mathf.Clamp01((worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);

		int xIndex = Mathf.RoundToInt((gridNodesX - 1) * percentX);
		int yIndex = Mathf.RoundToInt((gridNodesY - 1) * percentY);

		return grid[xIndex,yIndex];
	}

	private void OnDrawGizmos()
	{
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				if (path != null && path.Contains(n))
					Gizmos.color = Color.black;
				else 
					Gizmos.color = n.walkable ? Color.green : Color.red;
				Gizmos.DrawWireCube(n.worldPos, new Vector3(nodeDiameter, nodeDiameter, nodeDiameter));
			}
		}
	}

}
