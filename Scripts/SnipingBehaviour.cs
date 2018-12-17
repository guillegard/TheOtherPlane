using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipingBehaviour : MonoBehaviour, IEnemyBehaviour {
	
	public float detectionRadius = 7f;
	public bool fireOnlyIfInRange = true;
	public LayerMask detectionLayer;

	Enemy pawn;
	CircleCollider2D detectionTrigger;

	float attackCooldown;
	float currentAttackCD = 0;


	void Start()
	{
		pawn = GetComponent<Enemy>();
		attackCooldown = pawn.rangedCooldown;

		detectionTrigger = gameObject.AddComponent<CircleCollider2D>();
		detectionTrigger.isTrigger = true;
		detectionTrigger.radius = detectionRadius;

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
			{
				Fire();
			}
		}

	}

	private void Fire()
	{
		currentAttackCD = attackCooldown;
		pawn.RangedAttack(PlayerManager.instance.player.transform.position);
	}

	void OnDrawGizmos()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
