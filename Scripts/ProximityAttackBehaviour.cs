using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAttackBehaviour : MonoBehaviour, IEnemyBehaviour
{
	[HideInInspector]
	public Transform target;

	EnemyController controller;
	Enemy pawn;

	public float detectionRadius = 7f;
	public float attackRadius = 2f;
	public float attackCooldown = 4f; //in seconds
	public float pathReadjustCooldown = 0.5f; //in seconds

	float currentAttackCD = 0;
	float currentReadjustCD = 0;
	bool pursuing = false;      //Whether this enemy is trying to reach the player or not

	PathfindingAgent pawnAgent;
	/*CircleCollider2D detectionTrigger;
	CircleCollider2D attackTrigger;*/

	void Start()
	{
		target = PlayerManager.instance.player.transform;
		controller = GetComponent<EnemyController>();
		pawnAgent = GetComponent<PathfindingAgent>();

		if (controller != null)
		{
			pawn = controller.pawn;
		}

		/*detectionTrigger = gameObject.AddComponent<CircleCollider2D>();
		detectionTrigger.isTrigger = true;
		detectionTrigger.radius = detectionRadius;
		attackTrigger = gameObject.AddComponent<CircleCollider2D>();
		attackTrigger.isTrigger = true;
		attackTrigger.radius = attackRadius;*/
	}

	public void Tick()
	{
		//Player within detection radius
		float sqrDist = (target.position - pawn.gameObject.transform.position).sqrMagnitude;
		if (sqrDist <= detectionRadius * detectionRadius)
		{
			//Player within attack radius
			if (sqrDist <= attackRadius * attackRadius && currentAttackCD <= 0)
			{
				currentAttackCD = attackCooldown;
				currentReadjustCD = pathReadjustCooldown;

				pawn.Attack();
			}

			//Adjust path only if we can attack
			if (currentReadjustCD <= 0 && currentAttackCD <= 0)
			{
				Vector3 target = GetNearestAdyacent();
				pawnAgent.MoveTowards(target);
				currentReadjustCD = pathReadjustCooldown;
			}
		}

		//Process cooldowns
		if (currentAttackCD > 0)
			currentAttackCD -= Time.deltaTime;

		if (currentReadjustCD > 0)
			currentReadjustCD -= Time.deltaTime;

		/*
		if (currentAttackCD <= 0)
		{
			print("Enemy standing by");
			float sqrDist = (target.position - pawn.gameObject.transform.position).sqrMagnitude;
			if (sqrDist <= detectionRadius * detectionRadius)
			{
				print("Target detected " + target.position + ", " + target.gameObject.name);
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
					currentAttackCD = attackCooldown;
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
			currentAttackCD -= Time.deltaTime;
		}*/
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
