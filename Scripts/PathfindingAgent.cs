using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent : MonoBehaviour {

	Vector3 target;
	public float moveSpeed = 5;
	Vector3[] path;
	int targetIndex;
	
	public void MoveTowards(Vector3 target)
	{
		this.target = target;
		PathRequestManager.RequestPath(transform.position, target, OnPathFound);
	}

	public void Stop()
	{
		targetIndex = 0;
		StopCoroutine("FollowPath");
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccess)
	{
		if (pathSuccess)
		{
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		
		while(true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				//Reached target, end coroutine
				if (targetIndex >= path.Length)
				{
					targetIndex = 0;
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
			yield return null; //wait until next frame
		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one *.1f);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}
