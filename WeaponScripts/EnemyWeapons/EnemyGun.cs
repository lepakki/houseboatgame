using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : EnemyWeaponBase
{

    // Use this for initialization
    void Start()
    {
        createWeapon("Enemy gun", 1);
        

    }

    // Update is called once per frame
    void Update()
    {
        attackOrigin = GetComponentInParent<Transform>().position;
        
        //Shoot();

    }
}
