using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{
    public string nameE;

	[Header("Enemy Settings")]
    public float spiritReward;
    public bool isBoss;
    public float maxHp;
    public float maxSpirit;
    public float damage;
    public float cooldown;
	public float rangedCooldown;
	public float heavyDamage;
    public float heavyCooldown;
    public float specialDamage;
    public Projectile projectilePrefab;
	public LayerMask damagingLayer;
	public Vector2 defaultLookingDir;


    [Header("Control variables")]
	public float hp;
	public float spirit;
    public Weapon equippedWeapon;
    public Status status;
    
    [Header("UI variables")]
    public GameObject hpBar;

    [HideInInspector]
    public Vector2 direction;
    [HideInInspector]
    public Vector2 velocity;

    [Header("Adyacent objects")]
    public GameObject grabberU;
    public GameObject grabberD;
    public GameObject grabberR;
    public GameObject grabberL;

	[HideInInspector]
    public Animator anim { get; private set; }

	ContactFilter2D meleeAttackFilter = new ContactFilter2D();

	// Use this for initialization
	public void Awake()
	{
		hp = maxHp;
		spirit = maxSpirit;

		meleeAttackFilter.useTriggers = true;
		meleeAttackFilter.useLayerMask = true;
		meleeAttackFilter.layerMask = damagingLayer;
	}

	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		LookAt(transform.position + (Vector3)defaultLookingDir);
	}

	private void Update()
	{
	}

	public void MeleeAttack()
	{
		anim.SetTrigger("attack");

		Collider2D facingTrigger = GetFacingTrigger();
		Collider2D[] contacts = new Collider2D[10];
		int contactCount = facingTrigger.GetContacts(meleeAttackFilter, contacts);

		for (int i = 0; i < contactCount; i++)
		{
			if (contacts[i].isTrigger)
				continue;

			IDamageable damageableEntity = contacts[i].GetComponentInChildren<IDamageable>();

			if (damageableEntity != null)
			{
				damageableEntity.TakeDamage(damage, null);
			}
		}
	}

	public void RangedAttack(Vector3 target, bool fireFacingDir = false)
	{
		Vector3 right;
		if (fireFacingDir)
			right = direction;
		else
			right = (target - transform.position).normalized;

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

	public void LookAt(Vector3 target)
	{
		Vector2 lookDir = target - transform.position;
		float angle = (float)Mathf.Atan2(lookDir.y, lookDir.x);//- Mathf.PI / 2;
		angle = angle < 0 ? angle + Mathf.PI * 2 : angle;
		int dir = Mathf.RoundToInt(angle / (Mathf.PI / 2));

		switch (dir)
		{
			case 0:
			case 4:
				anim.SetTrigger("faceRight");
				direction = Vector2.right;
				break;
			case 1:
				anim.SetTrigger("faceUp");
				direction = Vector2.up;
				break;
			case 2:
				anim.SetTrigger("faceLeft");
				direction = Vector2.left;
				break;
			case 3:
				anim.SetTrigger("faceDown");
				direction = Vector2.down;
				break;
			default:
				print("Unexpected direction");
				Debug.Break();
				break;
		}
	}

	public void TakeDamage(float damage, Status s)
	{
		hp -= damage;
		float normalized = hp / maxHp;
		hpBar.transform.localScale = new Vector3(normalized, 1f);
		if (s != null)
			status = s;
		if (hp <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		LevelController.KillEnemy();
		PlayerManager.instance.player.GetSpirit(spiritReward);
		Destroy(gameObject);
	}

	

	Collider2D GetFacingTrigger()
	{
		if (direction == new Vector2(1, 0))
			return grabberR.GetComponent<Collider2D>();
		else if (direction == new Vector2(-1, 0))
			return grabberL.GetComponent<Collider2D>();
		else if (direction == new Vector2(0, 1))
			return grabberU.GetComponent<Collider2D>();
		else //dir == (0, -1)
			return grabberD.GetComponent<Collider2D>();
	}

	
}

public interface IDamageable
{
	void TakeDamage(float damage, Status s);
	void Die();
}
