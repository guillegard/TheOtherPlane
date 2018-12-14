using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit1 : MonoBehaviour {

    public float damage;
    public Status status;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    public void Move(int dir, Transform barrel, float normalDamage, float mult, Status s)
    {
        damage = normalDamage * mult;
        status = s;
        switch (dir)
        {
            case 1:
                GetComponent<Rigidbody2D>().AddForce(barrel.up * 500);
                break;

            case 2:
                GetComponent<Rigidbody2D>().AddForce(barrel.right * 500);
                break;

            case 3:
                GetComponent<Rigidbody2D>().AddForce(-barrel.up * 500);
                break;

            case 4:
                GetComponent<Rigidbody2D>().AddForce(-barrel.right * 500);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GrabberU" || collision.name == "GrabberD" || collision.name == "GrabberL" || collision.name == "GrabberR" || collision.name == "Player")
            return;
        if(collision.name.Length >= 7 && collision.name.Substring(0, 7) == "Trigger")
        {
            return;
        }

        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, status);
        }

        Destroy(this.gameObject);
    }
}
