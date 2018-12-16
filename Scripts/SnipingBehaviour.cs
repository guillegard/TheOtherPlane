using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipingBehaviour : MonoBehaviour, IEnemyBehaviour {
	
	public float detectionRadius = 7f;
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
	}

	private void Update()
	{
		if (currentAttackCD > 0)
			currentAttackCD -= Time.deltaTime;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (detectionTrigger.IsTouchingLayers(detectionLayer))
		{
			if (currentAttackCD <= 0)
			{
				currentAttackCD = attackCooldown;
				pawn.RangedAttack();
			}
		}
	}

	void OnDrawGizmos()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
