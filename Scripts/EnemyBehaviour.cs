using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour {

	public Transform target;
	public EnemyController controller;

	protected Enemy pawn;

	public abstract void Tick();
}
