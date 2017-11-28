using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeapon : MonoBehaviour {

    Weapons weapons;
    //Gun gun;

	// Use this for initialization
	void Start () {
        weapons = FindObjectOfType<Weapons>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newWeapon = new GameObject("Added Weapon");
            newWeapon.AddComponent<Gun>();
            weapons.addWeapon(newWeapon);
        }
	}
}
