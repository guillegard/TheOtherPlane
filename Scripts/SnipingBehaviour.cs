using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnipingBehaviour : MonoBehaviour, IEnemyBehaviour {
	
	public float detectionRadius = 7f;
	public bool fireOnlyIfInRange = true;
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
			if (currentAttackCD <= 0)
				Fire();

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
		Vector3 target = fireOnlyIfInRange ? PlayerManager.instance.player.transform.position : (Vector3)pawn.direction;
		currentAttackCD = attackCooldown;
		pawn.RangedAttack(target);
	}

	void OnDrawGizmosSelected()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
