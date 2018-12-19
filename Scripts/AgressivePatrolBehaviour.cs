using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolBehaviour))]
[RequireComponent(typeof(IEnemyAgressiveBehaviour))]
public class AgressivePatrolBehaviour : MonoBehaviour {

	PatrolBehaviour patrolBehaviour;
	IEnemyComplexAttackBehaviour complexEngageBehaviour;
	IEnemyAgressiveBehaviour engageBehaviour;

	bool isUsingComplexBehaviour = false;

	// Use this for initialization
	void Start () {

		patrolBehaviour = GetComponent<PatrolBehaviour>();

		IEnemyComplexAttackBehaviour[] complexBehaviours = GetComponents<IEnemyComplexAttackBehaviour>();
		if (complexBehaviours.Length > 0)
		{
			complexEngageBehaviour = complexBehaviours[0];
			complexEngageBehaviour.OnPlayerDetected += Engage;
			complexEngageBehaviour.OnPlayerUnDetected += Patrol;

			isUsingComplexBehaviour = true;
		}
		else
		{
			engageBehaviour = GetComponent<IEnemyAgressiveBehaviour>();

			engageBehaviour.OnPlayerDetected += Engage;
			engageBehaviour.OnPlayerUnDetected += Patrol;

		}

		patrolBehaviour.enabled = true;
		Behaviour behaviour = ((isUsingComplexBehaviour ? complexEngageBehaviour : engageBehaviour)) as Behaviour;
		behaviour.enabled = true;
	}

	void Patrol()
	{
		patrolBehaviour.enabled = true;
	}

	void Engage()
	{
		patrolBehaviour.enabled = false;
		patrolBehaviour.agent.Stop();
	}
}
