using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

	public float detectionRadius = 5f;

	Transform target;
	Enemy pawn;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		pawn = GetComponent<Enemy>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
