using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile {

    public override void Start()
    {
        base.Start();
        weapon.projectilePiercing = true;
        //This ramps up damage
        //weapon.projectileDamage += 100;
        //increase();
        
    }

    void increase()
    {
        weapon.projectileDamage += 50;
    }

}
