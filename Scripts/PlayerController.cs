using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject character;
    Character characterScript;
	// Use this for initialization
	void Start () {
        characterScript = character.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Horizontal"))
        {
            characterScript.MoveRight(Input.GetAxis("Horizontal"), 1);
        }
    }
}
