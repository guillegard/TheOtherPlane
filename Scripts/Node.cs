using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

	public bool walkable;
	public Vector3 worldPos;

	//The indices in the grid of this point
	public int gridX;
	public int gridY;

	public int gCost;		//Distance from node to begin node
	public int hCost;       //Distance from node to end node (heuristic)
	public Node parent;     //The node that most efficiently reaches this node
	int heapIndex;

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public int HeapIndex
	{
		get { return heapIndex; }
		set { heapIndex = value; }
	}


	public Node(bool _walkable, Vector3 _worldPos, int gridX, int gridY)
	{
		walkable = _walkable;
		worldPos = _worldPos;

		this.gridX = gridX;
		this.gridY = gridY;
	}

	public int CompareTo(Node other)
	{
		int comparison = fCost.CompareTo(other.fCost);

		return comparison == 0 ? hCost.CompareTo(other.hCost) : -comparison;
	}
	
}
