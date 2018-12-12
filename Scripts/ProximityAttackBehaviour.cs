using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityAttackBehaviour : EnemyBehaviour
{

	public float detectionRadius = 7;
	public float attackRadius = 2;
	public float cooldownTime = 4;//in seconds

	float currentCooldown = 0;
	bool pursuing = false;      //Whether this enemy is trying to reach the player or not
	NavMeshAgent pawnAgent;

	void Start()
	{
		target = PlayerManager.instance.player.transform;

		if (controller != null)
		{
			pawn = controller.pawn;
			pawnAgent = pawn.gameObject.GetComponent<NavMeshAgent>();
		}
	}

	public override void Tick()
	{
		if (currentCooldown <= 0)
		{
			float sqrDist = (target.position - pawn.gameObject.transform.position).sqrMagnitude;
			if (sqrDist <= detectionRadius * detectionRadius)
			{
				if (!pursuing)
				{
					pursuing = true;

					Vector3 target = GetNearestAdyacent();
					pawnAgent.SetDestination(target);
					pawnAgent.isStopped = false;
				}
				else if (pursuing && sqrDist <= attackRadius * attackRadius)
				{
					pawn.Attack();
					pursuing = false;
					pawnAgent.isStopped = true;
					currentCooldown = cooldownTime;
				}
			}
			else
			{
				pursuing = false;
				pawnAgent.isStopped = true;
			}
		}
		else
		{
			currentCooldown -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}

	Vector3 GetNearestAdyacent()
	{
		Transform[] adyacents = target.gameObject.GetComponent<Player>().GetAdyacentTransforms();
		Vector3 closest = new Vector3(0,0,0);
		float closestSqrDist = Mathf.Infinity;

		foreach(Transform t in adyacents)
		{
			float sqrDist = (pawn.gameObject.transform.position - t.position).sqrMagnitude;
			
			if (sqrDist <= closestSqrDist)
			{
				closestSqrDist = sqrDist;
				closest = t.position;
			}
		}
		return closest;
	}
}
