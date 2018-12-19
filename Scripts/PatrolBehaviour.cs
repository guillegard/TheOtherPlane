using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PatrolBehaviour : MonoBehaviour, IEnemyBehaviour {

	public Vector3[] patrolPoints;
	public bool shouldLoopAround;
	public LayerMask damageOnTouchLayer;
	public float proximityTolerance = 0.01f;

	int currentTargetIndex = 0;
	bool goingForward = true;
	bool beganPlay = false;

	[HideInInspector]
	public PathfindingAgent agent { get; private set; }
	Enemy pawn;

	private void Start()
	{
		//Transform vectors to world space
		for (int i = 0; i < patrolPoints.Length; i++)
			patrolPoints[i] += transform.position;

		beganPlay = true;

		pawn = GetComponent<Enemy>();
		agent = GetComponent<PathfindingAgent>();
		agent.MoveTowards(patrolPoints[currentTargetIndex]);
	}

	// Update is called once per frame
	void Update () {
		if ((patrolPoints[currentTargetIndex] - transform.position).magnitude <= proximityTolerance)
		{
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
			agent.MoveTowards(patrolPoints[currentTargetIndex]);
		}
		else if (!agent.isMoving)
		{
			agent.MoveTowards(patrolPoints[currentTargetIndex]);

		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (((1 << collision.gameObject.layer) & damageOnTouchLayer.value) != 0)
		{
			IDamageable damageableEntity = collision.gameObject.GetComponent<IDamageable>();

			if (damageableEntity != null)
				damageableEntity.TakeDamage(pawn.damage, null);
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
