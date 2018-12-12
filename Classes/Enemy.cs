using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

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

	// Use this for initialization
	public override void Start () {
		base.Start();
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
