using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {

    public Transform projectileOrigin;

	// Use this for initialization
	void Start () {
        createWeapon("Gun");
        Transform lel = GameObject.Find("PH_Top").GetComponent<Transform>();
        projectileOrigin = lel;

	}
	
	// Update is called once per frame
	void Update () {
        attackOrigin = projectileOrigin.position;//GetComponentInParent<Transform>().position;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
            Debug.Log("Attack origin = " + attackOrigin);
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            RerollWeaponStats();
        }
	}
}
