using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipingEnemy : MonoBehaviour {

	[HideInInspector]
	public Character target;

	EnemyController controller;
	Enemy pawn;

	public float attackCooldown = 4f; //in seconds
	public float pathReadjustCooldown = 1f; //in seconds

    CircleCollider2D detectionTrigger;


	float currentAttackCD = 0;
	float currentReadjustCD = 0;
	bool pursuing = false;      //Whether this enemy is trying to reach the player or not

	PathfindingAgent pawnAgent;
}
