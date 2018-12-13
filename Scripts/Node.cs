using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public bool walkable;
	public Vector3 worldPos;

	//The indices in the grid of this point
	public int gridX;
	public int gridY;

	public int gCost;		//Distance from node to begin node
	public int hCost;       //Distance from node to end node (heuristic)
	public Node parent;		//The node that most efficiently reaches this node

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public Node(bool _walkable, Vector3 _worldPos, int gridX, int gridY)
	{
		walkable = _walkable;
		worldPos = _worldPos;

		this.gridX = gridX;
		this.gridY = gridY;
	}

	
}
