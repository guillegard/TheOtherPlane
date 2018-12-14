﻿using System.Collections;
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
        if (Input.GetButton("Horizontal"))
        {
            characterScript.MoveRight(Input.GetAxis("Horizontal"));
        }

        else if (Input.GetButton("Vertical"))
        {
            //Debug.Log("KEYDOWN");
            //Debug.Log(Input.GetAxis("Vertical"));
            characterScript.MoveUp(Input.GetAxis("Vertical"));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            characterScript.Attack();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            characterScript.SpecialAttack();
        }

        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            characterScript.StopMove();

        if (Input.GetButtonDown("Submit"))
        {
            characterScript.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            characterScript.EquipSpirit(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            characterScript.EquipSpirit(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            characterScript.EquipSpirit(3);
        }

    }
}
