using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnipingBehaviour))]
[RequireComponent(typeof(ProximityAttackBehaviour))]

public class RangedMeleeAttackerBehaviour : MonoBehaviour {

	SnipingBehaviour rangeBehaviour;
	ProximityAttackBehaviour meleeBehaviour;
	
	void Start () {
		rangeBehaviour = GetComponent<SnipingBehaviour>();
		meleeBehaviour = GetComponent<ProximityAttackBehaviour>();

		meleeBehaviour.OnPlayerDetected += PlayerInMeleeRange;
		meleeBehaviour.OnPlayerUnDetected += PlayerOutMeleeRange;

		rangeBehaviour.enabled = true;
		meleeBehaviour.enabled = true;

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
