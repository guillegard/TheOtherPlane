﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //public variables
    public float moveSpeed;
    public float maxHp;
    public float maxSpirit;
    public float hp;
    public float spirit;
    public Weapon equippedWeapon;
    int equippedSpecial = -1;
    public Status status;
    public float damage;
    public float cooldown;
	public float heavyCooldown;
    public float specialDamage;

	[HideInInspector]
    public Vector2 direction;
	[HideInInspector]
	public Vector2 velocity;

    public GameObject grabberU;
    public GameObject grabberD;
    public GameObject grabberR;
    public GameObject grabberL;

    //private variables
    private Animator anim;
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    private bool hasKey = false;

    public Rigidbody2D spirit1Prefab;
    public Transform barrelUEnd;
    public Transform barrelDEnd;
    public Transform barrelREnd;
    public Transform barrelLEnd;

    public void Attack () {
        anim.SetTrigger("attack");
        RaycastHit2D[] bodies;
        if (up)
        {
            bodies = Physics2D.BoxCastAll(grabberU.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
               for(int i = 1; i < bodies.Length; i++)
               {
                    GameObject enemy = bodies[i].collider.gameObject;
                    if(enemy.GetComponent<Enemy>() != null && GetComponent<Enemy>() == null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damage, null, this.gameObject);
                    }
               }
            }

        }

        if (down)
        {
            bodies = Physics2D.BoxCastAll(grabberD.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                for (int i = 1; i < bodies.Length; i++)
                {
                    GameObject enemy = bodies[i].collider.gameObject;
                    if (enemy.GetComponent<Enemy>() != null && GetComponent<Enemy>() == null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damage, null, this.gameObject);
                    }
                }
            }

        }

        if (right)
        {
            bodies = Physics2D.BoxCastAll(grabberR.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                for (int i = 1; i < bodies.Length; i++)
                {
                    GameObject enemy = bodies[i].collider.gameObject;
                    if (enemy.GetComponent<Enemy>() != null && GetComponent<Enemy>() == null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damage, null, this.gameObject);
                    }
                }
            }

        }

        if (left)
        {
            bodies = Physics2D.BoxCastAll(grabberL.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                for (int i = 1; i < bodies.Length; i++)
                {
                    GameObject enemy = bodies[i].collider.gameObject;
                    if (enemy.GetComponent<Enemy>() != null && GetComponent<Enemy>() == null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damage, null, this.gameObject);
                    }
                }
            }
        }
    }

    public void HeavyAttack () {

    }

    public void SpecialAttack () {
        if (equippedSpecial == -1)
            return;
    
        if(equippedSpecial == 1)
        {
            GameObject special = GameObject.Find("Spirits/Spirit1");
            if (special != null)
            {
                if (special.GetComponent<Special>().spiritCost > spirit)
                    return;
                spirit = spirit - special.GetComponent<Special>().spiritCost;
            }
            if (up)
            {
                Rigidbody2D rocketInstance;
                rocketInstance = Instantiate(spirit1Prefab, barrelUEnd.position, barrelUEnd.rotation) as Rigidbody2D;
                rocketInstance.AddForce(barrelUEnd.up * 500);
            }
            if (down)
            {
                Rigidbody2D rocketInstance;
                rocketInstance = Instantiate(spirit1Prefab, barrelDEnd.position, barrelDEnd.rotation) as Rigidbody2D;
                rocketInstance.AddForce(-barrelDEnd.up * 500);
            }
            if (right)
            {
                Rigidbody2D rocketInstance;
                rocketInstance = Instantiate(spirit1Prefab, barrelREnd.position, barrelREnd.rotation) as Rigidbody2D;
                rocketInstance.AddForce(barrelREnd.right * 500);
            }
            if (left)
            {
                Rigidbody2D rocketInstance;
                rocketInstance = Instantiate(spirit1Prefab, barrelLEnd.position, barrelLEnd.rotation) as Rigidbody2D;
                rocketInstance.AddForce(-barrelLEnd.right * 500);
            }
            
        }
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
                transform.Translate(Vector3.down * Time.deltaTime * 3);
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
                transform.Translate(Vector3.up * Time.deltaTime * 3);
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
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 3);
                    //grabber.transform.Translate(new Vector3(transform.position.x + 0.18f, transform.position.y, 0f));
                }
                    
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
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 3);
                }
                    
            }
        }
    }

    public void StopMove()
    {
        //Debug.Log("STOP");

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

    public void Interact()
    {
        //Debug.Log(up + " " + down + " " + right + " " + left);

        RaycastHit2D[] bodies;
        if (up)
        {
            bodies = Physics2D.BoxCastAll(grabberU.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if(bodies.Length > 1)
            {
                //Debug.Log(bodies[1].collider.name);
                ItemInteract(bodies[1].collider);
            }
            
        }

        if (down)
        {
            bodies = Physics2D.BoxCastAll(grabberD.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                //Debug.Log(bodies[1].collider.name);
                ItemInteract(bodies[1].collider);
            }

        }

        if (right)
        {
            bodies = Physics2D.BoxCastAll(grabberR.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                //Debug.Log(bodies[1].collider.name);
                ItemInteract(bodies[1].collider);
            }

        }

        if (left)
        {
            bodies = Physics2D.BoxCastAll(grabberL.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                //Debug.Log(bodies[1].collider.name);
                ItemInteract(bodies[1].collider);
            }
        }
    }

    void ItemInteract(Collider2D collider)
    {

        if (collider.gameObject.GetComponent<Item>().type == Item.Type.Key)
        {
            Destroy(collider.gameObject);
            hasKey = true;
        }

        if (collider.gameObject.GetComponent<Item>().type == Item.Type.Door && hasKey)
        {
            Destroy(collider.gameObject);
            hasKey = false;
        }

        if (collider.gameObject.GetComponent<Item>().type == Item.Type.Chest)
        {
            string item = collider.gameObject.GetComponent<Item>().OpenChest();
            if(item != null)
            {
                
            }
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.GetComponent<Item>().type == Item.Type.Special)
        {
            Item col = collider.GetComponent<Item>();
            Debug.Log(col.itemName);
            GameObject special = GameObject.Find("Spirits/"+col.itemName);
            if(special != null)
            {
                col.spiritDialog();
                special.GetComponent<Special>().unlocked = true;
                Destroy(collider.gameObject);
            }
        }
    }

	// Use this for initialization
	public virtual void Start () {
        anim = GetComponent<Animator>();
        right = true;
	}

    public GameObject SpecialIsUnlocked(int sp)
    {
        GameObject special = GameObject.Find("Spirits/Spirit" + sp);
        if(special != null && special.GetComponent<Special>().unlocked)
        {
            return special;
        }
        else
        {
            return null;
        }
    }

    public void EquipSpirit(int sp)
    {
        if(sp == equippedSpecial)
        {
            return;
        }
        GameObject special = SpecialIsUnlocked(sp);
        if(special != null)
        {
            equippedSpecial = sp;
            //Debug.Log("Equipped");
        }
    }

    public void TakeDamage(float damage, Status s, GameObject didDamage, bool special)
    {
        hp -= damage;
        if(s != null)
            status = s;
        if(hp <= 0)
        {
            Die(didDamage);
        }
    }

    public void Die(GameObject didDamage)
    {
        if(GetComponent<Enemy>() == null)
        {

        }
        else
        {
            didDamage.GetComponent<Character>().GetSpirit(this.gameObject.GetComponent<Enemy>().spiritReward);
            Destroy(this.gameObject);
        }
    }

    public void GetSpirit(float reward)
    {
        spirit += reward;
        if(spirit > maxSpirit)
        {
            spirit = maxSpirit;
        }
    }
	
	// Update is called once per frame
	public virtual void Update () {
        
    }

	public Transform[] GetAdyacentTransforms()
	{
		return new Transform[] { grabberD.transform, grabberL.transform, grabberR.transform, grabberU.transform};
	}
}
