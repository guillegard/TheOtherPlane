using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SnipingBehaviour))]
[RequireComponent(typeof(ProximityAttackBehaviour))]

public class RangedMeleeAttackerBehaviour : MonoBehaviour, IEnemyComplexAttackBehaviour {

	public event Action OnPlayerDetected;
	public event Action OnPlayerUnDetected;

	SnipingBehaviour rangeBehaviour;
	ProximityAttackBehaviour meleeBehaviour;
	
	void Start () {
		rangeBehaviour = GetComponent<SnipingBehaviour>();
		meleeBehaviour = GetComponent<ProximityAttackBehaviour>();

		meleeBehaviour.OnPlayerDetected += PlayerInMeleeRange;
		meleeBehaviour.OnPlayerUnDetected += PlayerOutMeleeRange;

		rangeBehaviour.OnPlayerDetected += PlayerInFireRange;
		rangeBehaviour.OnPlayerUnDetected += PlayerOutFireRange;

		rangeBehaviour.enabled = true;
		meleeBehaviour.enabled = false;
	}

	void PlayerInFireRange()
	{
		if (OnPlayerDetected != null)
			OnPlayerDetected();

		meleeBehaviour.enabled = true;
	}

	void PlayerOutFireRange()
	{
		if (OnPlayerUnDetected != null)
			OnPlayerUnDetected();

		meleeBehaviour.enabled = false;
	}

	void PlayerInMeleeRange()
	{
		rangeBehaviour.enabled = false;
	}

	void PlayerOutMeleeRange()
	{
		rangeBehaviour.enabled = true;
	}

}
