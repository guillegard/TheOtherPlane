using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public string nameE;
    public float spiritReward;
    public float moveSpeedMultiplier;
    public float hpMultiplier;
    public float damageMultipler;
    public float heavyDamageMultiplier;
    public float coolDownMultiplier;
    public float heavyCooldownMultiplier;
    public bool isBoss;
    public float specialMultiplier;

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

    public void Attack()
    {
        anim.SetTrigger("attack");
        RaycastHit2D[] bodies;
        if (up)
        {
            bodies = Physics2D.BoxCastAll(grabberU.transform.position, new Vector2(0.1f, 0.1f), 0, new Vector2(0, 1), 0.1f);
            if (bodies.Length > 1)
            {
                for (int i = 1; i < bodies.Length; i++)
                {
                    GameObject enemy = bodies[i].collider.gameObject;
                    if (enemy.GetComponent<Character>() != null)
                    {
                        enemy.GetComponent<Character>().TakeDamage(damage, null);
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
                    if (enemy.GetComponent<Character>() != null)
                    {
                        enemy.GetComponent<Character>().TakeDamage(damage, null);
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
                    if (enemy.GetComponent<Character>() != null)
                    {
                        enemy.GetComponent<Character>().TakeDamage(damage, null);
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
                    if (enemy.GetComponent<Character>() != null)
                    {
                        enemy.GetComponent<Character>().TakeDamage(damage, null);
                    }
                }
            }
        }
    }

    public void TakeDamage(float damage, Status s)
    {
        hp -= damage;
        if (s != null)
            status = s;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().GetSpirit(spiritReward);
        Destroy(this.gameObject);   
    }



    // Use this for initialization
    public  void Start () {
		
	}

	// Update is called once per frame
	public void Update () {
		
	}
}
