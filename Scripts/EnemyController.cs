using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {

	public Enemy pawn;

	public IEnemyBehaviour enemyBehaviour;

	// Use this for initialization
	void Start () {
		enemyBehaviour = GetComponent<IEnemyBehaviour>();

		if (pawn == null)
		{
			print("Enemy controller does not have an enemy to control");
		}


		if (enemyBehaviour == null) {
			print("Enemy controller does not have a behaviour");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (enemyBehaviour != null)
			enemyBehaviour.Tick();
	}
}
