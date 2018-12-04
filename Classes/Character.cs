using System.Collections;
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

    void Attack () {

    }

    void HeavyAttack () {

    }

    void SpecialAttack () {

    }

    public void MoveUp (float value, float deltaTime) {
        if(value < 0)
        {
            Debug.Log("MOVE DOWN");
        }else if(value > 0)
        {
            Debug.Log("MOVE UP");
        }
    }

    public void MoveRight (float value, float deltaTime) {
        if (value > 0)
        {
            Debug.Log("MOVE RIGHT");
        }
        else if (value < 0)
        {
            Debug.Log("MOVE LEFT");
        }

    }


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
