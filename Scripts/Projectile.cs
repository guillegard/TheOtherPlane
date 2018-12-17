using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float damage = 4;
	public float speed = 2;
	public float lifeTime = 100;
	public Status appliedStatus;
	public LayerMask damageLayer;

	Collider2D detectionCollider;

	private void Awake()
	{
		detectionCollider = GetComponent<Collider2D>();

		Destroy(gameObject, lifeTime);
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate(transform.right * speed * Time.deltaTime);
		transform.position += transform.right * speed * Time.deltaTime;
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (detectionCollider.IsTouchingLayers(damageLayer))
		{
			Character player = collider.gameObject.GetComponent<Character>();
			if (player != null)
			{
				player.TakeDamage(damage, appliedStatus);
				Destroy(gameObject);
				return;
			}
		}

		if (!collider.isTrigger)
			Destroy(gameObject);
	}

}
