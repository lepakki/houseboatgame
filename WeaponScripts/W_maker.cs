using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class W_maker : MonoBehaviour {

    //Get WeaponStatsPanel
    public GameObject weaponStatsPanel;
    public Text weaponNameText;


    //W_new wnew;
    public GameObject w;
    public GameObject parent;

    void Awake()
    {
        parent = GameObject.Find("PH_Top");
        weaponStatsPanel = GameObject.Find("WeaponStatsPanel");
        weaponNameText = weaponStatsPanel.GetComponentInChildren<Text>();
        //GameObject w = new GameObject("PlaceHolder");
    }

	// Use this for initialization
	void Start () {
        createWeapon();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(w);
            createWeapon();
        }
	}

    void initializePanel(GameObject w)
    {
        //weaponNameText.text = w.gameObject.name + '\n' + w.gameObject.GetComponent<W_new>().projectileDamage;
    }

    void createWeapon()
    {
        
        w = new GameObject("thisweapon");
        w.AddComponent<W_new>();
        w.GetComponent<W_new>().enabled = true;
        initializePanel(w);
        if (parent != null)
        {
            w.transform.SetParent(parent.transform);
            w.transform.position = parent.transform.position;
        }
        //w.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        //w.transform.position = GetComponentInParent<Transform>().position;
    }
}
