using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

	public float detectionRadius = 5f;

	[HideInInspector]
	public Enemy pawn;

	NavMeshAgent agent;
	public EnemyBehaviour enemyBehaviour;

	// Use this for initialization
	void Start () {
		pawn = GetComponent<Enemy>();
		agent = GetComponent<NavMeshAgent>();

		if (enemyBehaviour != null) {
			enemyBehaviour.controller = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyBehaviour != null)
			enemyBehaviour.Tick();
	}
}
