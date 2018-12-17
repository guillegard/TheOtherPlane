using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProximityAttackBehaviour : MonoBehaviour, IEnemyBehaviour
{
	[HideInInspector]
	public Character target;

	public float detectionRadius = 7f;
	public float attackRadius = 2f;
	public float pathReadjustCooldown = 1f; //in seconds

	public event Action OnPlayerDetected;
	public event Action OnPlayerUnDetected;

	EnemyController controller;
	Enemy pawn;
	PathfindingAgent pawnAgent;
	/*CircleCollider2D detectionTrigger;
	CircleCollider2D attackTrigger;*/

	float currentAttackCD = 0;
	float currentReadjustCD = 0;
	bool pursuing = false;      //Whether this enemy is trying to reach the player or not
	

	void Start()
	{
		target = PlayerManager.instance.player;
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

	void Update()
	{
		//Player within detection radius
		float dist = (target.transform.position - transform.position).magnitude;
		if (dist <= detectionRadius )
		{
			//Player within attack radius
			if ((dist <= attackRadius ) && currentAttackCD <= 0)
			{
				currentAttackCD = pawn.cooldown;

				pawn.MeleeAttack();
				print("Enemy attacking");

				pursuing = false;
			}

			//Adjust path only if we can attack
			if (currentReadjustCD <= 0 && currentAttackCD <= 0)
			{
				pursuing = true;
				Vector3 target = GetNearestAdyacent();
				pawnAgent.MoveTowards(target);
				print("Enemy pursuing to" + target);
				currentReadjustCD = pathReadjustCooldown;
			}
		}
		else if (pursuing)
		{
			print("Player out of range");
			pursuing = false;
			pawnAgent.Stop();
		}

		//Attack cooldown
		if (currentAttackCD > 0)
			currentAttackCD -= Time.deltaTime;

		//Readjustment cooldown
		if (currentReadjustCD > 0)
			currentReadjustCD -= Time.deltaTime;


	}

	void OnDrawGizmosSelected()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}

	Vector3 GetNearestAdyacent()
	{
		Transform[] adyacents = target.GetAdyacentTransforms();
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
