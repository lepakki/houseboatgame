using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase {

    EnemyGun eg;
    DrawRayToPlayer drtp;

	// Use this for initialization
	public override void Start () {
        health = 550;
        startingHealth = health;
        SetHealth(startingHealth);
        maximumDistanceFromOrigin = 10;
        shootingDistance = 9f;
        selectedClass = enemyClass.Shooter;
        drtp = GetComponent<DrawRayToPlayer>();
        eg = GetComponentInChildren<EnemyGun>();
    }

    public override void Update()
    {
        base.Update();
        //transform.LookAt(player.transform);
        if(selectedState == state.Shoot)
        {
            eg.Shoot();
        }
        
    }

}
