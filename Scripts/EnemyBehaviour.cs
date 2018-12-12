using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour {

	[HideInInspector]
	public Transform target;
	[HideInInspector]
	public EnemyController controller;

	protected Enemy pawn;

	public abstract void Tick();
}
