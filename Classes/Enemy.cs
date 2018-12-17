using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

	[Header("Enemy Settings")]
	public string nameE;
    public float spiritReward;
    public bool isBoss;
    public float maxHp;
    public float maxSpirit;
	public float hp;
	public float spirit;
    public Weapon equippedWeapon;
    public Status status;
    public float damage;
    public float cooldown;
	public float rangedCooldown;
	public float heavyDamage;
    public float heavyCooldown;
    public float specialDamage;

    [HideInInspector]
    public Vector2 direction = new Vector2(1, 0);
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

    public Projectile projectilePrefab;


	// Use this for initialization
	public void Start()
	{
		anim = GetComponentInChildren<Animator>();
		right = true;
	}


	public void MeleeAttack()
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

	public void RangedAttack(Vector3 target)
	{
		Vector3 right = (target - transform.position).normalized;
		Vector3 forward = transform.forward;

		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(forward, Vector3.Cross(forward, right)));
		Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}

	public void HeavyAttack()
	{

	}

	public void Special()
	{

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
        PlayerManager.instance.player.GetSpirit(spiritReward);
        Destroy(this.gameObject);   
    }
}
