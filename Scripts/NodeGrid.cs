using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodeGrid : MonoBehaviour {

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
		Vector3 worldTopLeft = transform.position + Vector3.up * gridWorldSize.y / 2 - Vector3.right * gridWorldSize.x / 2;

		for (int y = 0; y < gridNodesY; y++)
		{
			for (int x = 0; x < gridNodesX; x++)
			{
				Vector3 worldPos = worldTopLeft + (Vector3.right * (x * nodeDiameter + nodeRadius)) + (Vector3.down * (y * nodeDiameter + nodeRadius));
				bool walkable = Physics.CheckBox(worldPos, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, unwalkableMask.value);

				grid[x, y] = new Node(walkable, worldPos);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = n.walkable ? Color.green : Color.red;
				Gizmos.DrawWireCube(n.worldPos, new Vector3(nodeDiameter, nodeDiameter, nodeDiameter));
			}
		}
	}

}
