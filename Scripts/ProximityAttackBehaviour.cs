using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProximityAttackBehaviour : MonoBehaviour, IEnemyAgressiveBehaviour
{
	[HideInInspector]
	public Character target;

	public float detectionRadius = 7f;
	public float attackRadius = 2f;
	public float pathReadjustCooldown = 1f; //in seconds
	public bool engageOnlyIfVisible = true;

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
	bool playerDetected = false;
	

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
			/*if (engageOnlyIfVisible)
			{
				RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position, detectionRadius);

				//First object hit is not our target
				if (hit.collider.gameObject != target.gameObject)
					return;
			}*/

			if (!playerDetected)
			{
				playerDetected = true;
				if (OnPlayerDetected != null)
					OnPlayerDetected();
			}

			//Player within attack radius
			if ((dist <= attackRadius ) && currentAttackCD <= 0)
			{
				currentAttackCD = pawn.cooldown;

				pawn.LookAt(target.transform.position);
				pawn.MeleeAttack();
				pawnAgent.Stop();
				pursuing = false;

			}

			//Adjust path only if we can attack
			if (currentReadjustCD <= 0 && currentAttackCD <= 0)
			{
				pursuing = true;
				Vector3 target = GetNearestAdyacent();
				pawnAgent.MoveTowards(target);
				currentReadjustCD = pathReadjustCooldown;
			}
		}
		else if (playerDetected)
		{
			playerDetected = false;
			pursuing = false;
			pawnAgent.Stop();

			if (OnPlayerUnDetected != null)
				OnPlayerUnDetected();
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
