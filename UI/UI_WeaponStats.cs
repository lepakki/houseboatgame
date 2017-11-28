using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponStats : MonoBehaviour {

    Gun gun;
    Text text;
    string weaponStats;
	// Use this for initialization
	void Start () {
        gun = FindObjectOfType<Gun>();
        text = GetComponentInChildren<Text>();

        

	}
	
	// Update is called once per frame
	void Update () {
        weaponStats = gun.getStats();
        text.text = weaponStats;
    }
}
