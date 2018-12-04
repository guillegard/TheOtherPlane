using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public enum StatusCondition {
        NORMAL,
        FROZEN,
        BURNED,
        POISONED,
        DEFEATED,
        PANIC
    };

    public StatusCondition currentStatus = StatusCondition.NORMAL;
    public StatusCondition givenStatus;

    public string[] statusName = { "Normal", "Frozen", "Burned", "Poisoned", "Afraid", "Defeated" };

    public float[] statusMultiplier = { 1.0f, 0.5f, 0.5f, 0.125f, 0.0f, 0.0f };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
