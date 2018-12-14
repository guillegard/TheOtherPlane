using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PatrolBehaviour : MonoBehaviour, IEnemyBehaviour {

	public Vector3[] patrolPoints;
	public bool shouldLoopAround;
	public float proximityTolerance = 0.01f;

	int currentTargetIndex = 0;
	bool goingForward = true;
	PathfindingAgent agent;
	bool beganPlay = false;

	private void Start()
	{
		//Transform vectors to world space
		for (int i = 0; i < patrolPoints.Length; i++)
			patrolPoints[i] += transform.position;

		beganPlay = true;

		agent = GetComponent<PathfindingAgent>();
		agent.MoveTowards(patrolPoints[currentTargetIndex]);
	}

	// Update is called once per frame
	void Update () {
		if ((patrolPoints[currentTargetIndex] - transform.position).sqrMagnitude <= proximityTolerance)
		{
			agent.Stop();
			if (shouldLoopAround)
				currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Length;
			else
			{
				if (goingForward)
				{
					if (currentTargetIndex + 1 >= patrolPoints.Length)
					{
						currentTargetIndex--;
						goingForward = false;
					}
					else
						currentTargetIndex++;
				}
				else
				{
					if (currentTargetIndex - 1 < 0)
					{
						currentTargetIndex++;
						goingForward = true;
					}
					else
						currentTargetIndex--;
				}
			}
			print("New patrol target index: " + currentTargetIndex);
			agent.MoveTowards(patrolPoints[currentTargetIndex]);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (!this.enabled)
			return;

		for (int i = 0; i < patrolPoints.Length; i++) 
		{
			Vector3 drawPos = patrolPoints[i];
			if (!beganPlay)
				drawPos += transform.position;

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(drawPos, 0.1f);
			Handles.color = Color.black;
			Handles.Label(drawPos + Vector3.down * 0.05f, i.ToString());
		}
	}
}
