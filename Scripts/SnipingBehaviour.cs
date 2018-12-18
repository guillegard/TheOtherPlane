using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnipingBehaviour : MonoBehaviour, IEnemyBehaviour {
	
	public float detectionRadius = 7f;
	public bool fireOnlyIfInRange = true;
	public bool engageOnlyIfVisible = true;
	public LayerMask detectionLayer;

	public event Action OnPlayerDetected;
	public event Action OnPlayerUnDetected;

	Enemy pawn;
	CircleCollider2D detectionTrigger;

	float attackCooldown;
	float currentAttackCD = 0;
	bool attackingPlayer = false;

	private void Awake()
	{
		detectionTrigger = gameObject.AddComponent<CircleCollider2D>();
		detectionTrigger.isTrigger = true;
		detectionTrigger.radius = detectionRadius;
	}

	void Start()
	{
		pawn = GetComponent<Enemy>();
		attackCooldown = pawn.rangedCooldown;

		// Adjust radius to match
		var matrix = detectionTrigger.transform.localToWorldMatrix;
		var xAxisMag = matrix.GetColumn(0).magnitude;
		var yAxisMag = matrix.GetColumn(1).magnitude;
		var zAxisMag = matrix.GetColumn(2).magnitude;
		
		detectionTrigger.radius = detectionRadius / Mathf.Max(xAxisMag, yAxisMag, zAxisMag);
	}

	private void Update()
	{
		if (currentAttackCD > 0)
			currentAttackCD -= Time.deltaTime;

		if (detectionTrigger.IsTouchingLayers(detectionLayer) || !fireOnlyIfInRange)
		{
			/*if (engageOnlyIfVisible)
			{
				Vector3 detectedTarget = PlayerManager.instance.player.transform.position;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, (detectedTarget - transform.position).normalized, detectionRadius);

				//First object hit is not detectable => object is not visible
				if ((( 1 << (hit.collider.gameObject.layer + 1)) & detectionLayer.value) != 0)
					return;
			}*/

			if (currentAttackCD <= 0)
				Fire();

			if (fireOnlyIfInRange)
				pawn.LookAt(PlayerManager.instance.player.transform.position);

			if (!attackingPlayer && fireOnlyIfInRange)
			{
				attackingPlayer = true;
				if (OnPlayerDetected != null)
					OnPlayerDetected();
			}
		}
		else if (attackingPlayer)
		{
			attackingPlayer = false;
			if (OnPlayerUnDetected != null)
				OnPlayerUnDetected();
		}

	}

	private void Fire()
	{
		currentAttackCD = attackCooldown;
		pawn.RangedAttack(PlayerManager.instance.player.transform.position, !fireOnlyIfInRange);
	}

	void OnDrawGizmosSelected()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
