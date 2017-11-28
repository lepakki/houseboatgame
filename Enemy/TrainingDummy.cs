using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : Enemy {


	// Use this for initialization
	void Start () {
        health = 5000;
        startingHealth = health;
        InitializeHealthBar();
        
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}
}
