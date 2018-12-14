using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {

	[HideInInspector]
	public Enemy pawn;
	IEnemyBehaviour enemyBehaviour;

	// Use this for initialization
	void Awake () {
		enemyBehaviour = GetComponent<IEnemyBehaviour>();
		pawn = GetComponent<Enemy>();

		if (pawn == null)
		{
			print("Enemy controller does not have an enemy to control");
		}
		if (enemyBehaviour == null) {
			print("Enemy controller does not have a behaviour");
		}

	}
	
}
