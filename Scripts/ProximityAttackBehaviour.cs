using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAttackBehaviour : MonoBehaviour, IEnemyBehaviour
{
	[HideInInspector]
	public Transform target;

	EnemyController controller;
	Enemy pawn;

	public float detectionRadius = 7;
	public float attackRadius = 2;
	public float cooldownTime = 4;//in seconds

	float currentCooldown = 0;
	bool pursuing = false;      //Whether this enemy is trying to reach the player or not
	PathfindingAgent pawnAgent;

	void Start()
	{
		target = PlayerManager.instance.player.transform;
		controller = GetComponent<EnemyController>();
		pawnAgent = GetComponent<PathfindingAgent>();

		if (controller != null)
		{
			pawn = controller.pawn;
		}

	}

	public void Tick()
	{
		if (currentCooldown <= 0)
		{
			print("Enemy standing by");
			float sqrDist = (target.position - pawn.gameObject.transform.position).sqrMagnitude;
			if (sqrDist <= detectionRadius * detectionRadius)
			{
				print("Target detected " + target.position);
				if (!pursuing)
				{
					pursuing = true;
					
					Vector3 target = GetNearestAdyacent();
					print("Enemy pursuing to" + target);
					pawnAgent.MoveTowards(target);
				}
				else if (pursuing && sqrDist <= attackRadius * attackRadius)
				{
					print("Attacked target");
					pawn.Attack();
					pursuing = false;
					pawnAgent.Stop();
					currentCooldown = cooldownTime;
				}

			}
			else if (pursuing)
			{
				print("Target escaped");
				pursuing = false;
				pawnAgent.Stop();
			}
		}
		else
		{
			print("Enemy in cooldown");
			currentCooldown -= Time.deltaTime;
		}
	}

	void OnDrawGizmos()
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
