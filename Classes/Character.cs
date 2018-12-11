﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //public variables
    public float moveSpeed;
    public float hp;
    public float spirit;
    public Weapon equippedWeapon;
    public Special equippedSpecial;
    public Status status;
    public float damage;
    public float cooldown;
    public float specialDamage;
    public Vector2 direction;
    public Vector2 velocity;

    //private variables
    private Animator anim;
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    public void Attack () {
        anim.SetTrigger("attack");
    }

    void HeavyAttack () {

    }

    public void SpecialAttack () {
        anim.SetTrigger("special");
    }

    public void MoveUp (float value) {
        if (!IsAttacking())
        {
            anim.SetBool("isMoving", true);
            if (value < 0)
            {
                //Debug.Log("MOVE DOWN");
                if (!down)
                {
                    anim.SetTrigger("faceDown");
                    down = true;
                    up = false;
                    left = false;
                    right = false;
                }
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            else if (value > 0)
            {
                //Debug.Log("MOVE UP");
                if (!up)
                {
                    anim.SetTrigger("faceUp");
                    up = true;
                    left = false;
                    down = false;
                    right = false;
                }
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
    }

    public void MoveRight (float value) {
        if (!IsAttacking())
        {
            anim.SetBool("isMoving", true);
            if (value > 0)
            {
                //Debug.Log("MOVE RIGHT");
                if (!right)
                {
                    anim.SetTrigger("faceRight");
                    right = true;
                    up = false;
                    down = false;
                    left = false;
                }
                if (!IsAttacking())
                    transform.Translate(Vector3.right * Time.deltaTime);
            }
            else if (value < 0)
            {
                //Debug.Log("MOVE LEFT");
                if (!left)
                {
                    anim.SetTrigger("faceLeft");
                    left = true;
                    up = false;
                    down = false;
                    right = false;
                }
                if (!IsAttacking())
                    transform.Translate(Vector3.left * Time.deltaTime);
            }
        }
    }

    public void StopMove()
    {
        //Debug.Log("STOP");
        up = false;
        down = false;
        right = false;
        left = false;
        anim.SetBool("isMoving", false);
    }

    public bool IsAttacking()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_up") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_down") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_right") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_left") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_right") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_left") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_up") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_down"))
        {
            return true;
        }

        if (!(this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_up") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_down") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_right") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_left") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_right") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_left") ||
            this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_up") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("special_down")))
        {
            return false;
        }

        return true;
    }


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
