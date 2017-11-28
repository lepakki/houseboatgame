using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInWorldSpace : MonoBehaviour {

    Gun gun;
    Weapons weapons;
    GameObject lootWeapon;
    [SerializeField]
    public List<GameObject> loot;

    void OnMouseEnter()
    {
        Debug.Log("MOUSEOVER");
        lootWeapon = weapons.getWeaponAtIndex(1);
        if (!loot.Contains(lootWeapon))
        {
            loot.Add(lootWeapon);
        }
        
        //gameObject.AddComponent<weapons>().

    }

    void OnMouseDown()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Component ds = loot[0].GetComponent<Gun>();
            col.gameObject.AddComponent(ds.GetType());
        }
    }

	// Use this for initialization
	void Start () {
        weapons = FindObjectOfType<Weapons>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
