using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {


    //GameObject[] weapons = new GameObject[5];
    [SerializeField]
    public List<GameObject> weapons;

    public void addWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(transform);
        weapons.Add(weapon);
    }

    public GameObject getWeaponAtIndex(int index)
    {
        GameObject returnWeapon = weapons[index];
        return returnWeapon;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
