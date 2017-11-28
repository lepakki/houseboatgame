using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drooler : EnemyBase
{
    
    public override void Start()
    {
        health = 900;
        startingHealth = health;
        SetHealth(startingHealth);
        selectedClass = enemyClass.Drooler;
        //InitializeHealthBar();
        //InitializeEnemy();
    }
    
}
