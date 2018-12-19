using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IEnemyBehaviour
{
}

public interface IEnemyAgressiveBehaviour : IEnemyBehaviour
{
	event Action OnPlayerDetected;
	event Action OnPlayerUnDetected;
}

public interface IEnemyComplexAttackBehaviour : IEnemyAgressiveBehaviour
{

}
