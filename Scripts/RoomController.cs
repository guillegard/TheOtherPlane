using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public GameObject[] doors;
    public bool[] openDoors;
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleDoor(int[] doorIndexes)
    {
        foreach(int doorIndex in doorIndexes)
        {
            if (openDoors[doorIndex])
            {
                CloseDoor(doorIndex);
            }
            else
            {
                OpenDoor(doorIndex);
            }
        }
    }

    public void CloseDoor(int index)
    {
        doors[index].SetActive(true);
    }

    public void OpenDoor(int index)
    {
        doors[index].SetActive(false);
    }

    public void PutEnemy(int[] enemyIndexes)
    {
        foreach(int enemyIndex in enemyIndexes)
            enemies[enemyIndex].SetActive(true);
    }
}
