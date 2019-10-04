using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PathfindingAgent))]
public class FleeBehaviour : MonoBehaviour, IEnemyBehaviour {

	public float fleeingMoveSpeed = 10f;
	public float playerDetectionRadius = 7f;
	/*Time in seconds, in between path updates*/
	public float pathUpdateTime = 0.33f;

	public float collisionCheckDist = 2;

	public event Action playerDetected;
	public event Action playerUndetected;

	private Character player;
	private CircleCollider2D detectionCollider;
	private PathfindingAgent agent;
	private Enemy pawn;

	private float defaultMoveSpeed;
	private bool bIsFleeing = false;
	private float lastUpdateTime;

	void Awake()
	{
		detectionCollider = gameObject.AddComponent<CircleCollider2D>();
		detectionCollider.isTrigger = true;

		// Adjust radius to match
		var matrix = detectionCollider.transform.localToWorldMatrix;
		var xAxisMag = matrix.GetColumn(0).magnitude;
		var yAxisMag = matrix.GetColumn(1).magnitude;
		var zAxisMag = matrix.GetColumn(2).magnitude;

		detectionCollider.radius = playerDetectionRadius / Mathf.Max(xAxisMag, yAxisMag, zAxisMag);
	}

	// Use this for initialization
	void Start () {
		player = PlayerManager.instance.player;
		agent = GetComponent<PathfindingAgent>();
		pawn = GetComponent<Enemy>();


	}
	
	// Update is called once per frame
	void Update () {
		if (bIsFleeing && (lastUpdateTime + pathUpdateTime >= Time.time))
		{
			Vector2 runningDir = (transform.position - player.transform.position).normalized;
			Vector3 targetPos = (Vector3)(runningDir) * 10 + transform.position;
			agent.MoveTowards(targetPos);

			lastUpdateTime = Time.time;

			//Do raycast to avoid obstacles
			RaycastHit2D hit = Physics2D.Raycast(transform.position, pawn.direction, collisionCheckDist, LayerMask.NameToLayer("Unwalkable"));
			if (hit)
			{
				Vector2 moveDir = (Vector2)( Quaternion.Euler(0, 0, Mathf.RoundToInt(UnityEngine.Random.Range(1, 3)) * 90) * (Vector3)pawn.direction);

				agent.MoveTowards(transform.position + (Vector3)(moveDir.normalized * 10));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			bIsFleeing = true;

			defaultMoveSpeed = agent.moveSpeed;
			agent.moveSpeed = fleeingMoveSpeed;

			if (playerDetected != null)
				playerDetected();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			bIsFleeing = false;

			agent.moveSpeed = defaultMoveSpeed;

			if (playerUndetected != null)
				playerUndetected();
		}
	}

	void OnDrawGizmosSelected()
	{
		if (!this.enabled)
			return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

		Gizmos.color = Color.red;
		if (pawn)
			Gizmos.DrawLine(transform.position, transform.position + (Vector3)(pawn.direction * collisionCheckDist));
	}
}
