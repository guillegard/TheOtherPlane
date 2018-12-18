using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {

    public enum Type
    {
        EnemiesToggleDoor, Puzzle, PickSpiritEnemyAppear
    }

    public Type type;
    public GameObject colliderTrigger;
    public int deathEnemiesT;
    public int[] door;
    public GameObject roomController;
    public int[] enemyToPut;

    // Use this for initialization
    void Start () {
		if(type == Type.EnemiesToggleDoor)
        {
            LevelController.EnemyDies += TriggerEnemyDiesToggleDoor;
        }
        else
        {
            if(type == Type.PickSpiritEnemyAppear)
            {
                LevelController.PickSpirit += PickSpiritEnemyAppear;
            }
        }
	}
	
    public void PickSpiritEnemyAppear()
    {
        LevelController.PickSpirit -= PickSpiritEnemyAppear;
        roomController.GetComponent<RoomController>().PutEnemy(enemyToPut);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerEnemyDiesToggleDoor()
    {
        if( LevelController.killedEnemies >= deathEnemiesT )
        {
            LevelController.EnemyDies -= TriggerEnemyDiesToggleDoor;
            roomController.GetComponent<RoomController>().ToggleDoor(door);
        }
    }
}
