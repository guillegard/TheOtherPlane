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
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        right = true;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
