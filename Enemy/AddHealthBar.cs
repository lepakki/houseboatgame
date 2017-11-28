using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHealthBar : MonoBehaviour {

    //Temp code
    //public GameObject player;
    SpriteRenderer spr;
    Vector2 size;

    public GameObject owner;


    public GameObject hpBarBase;
    public Transform child;
    //public Transform parent;
    public Image hpBar;
    public Vector2 barPosition;
    public Bounds bounds;

	// Use this for initialization
	void Start () {
        
        CreateHealthBar();

        
	}

    public Image getHpBar()
    {
        return hpBar;
    }

    public void CreateHealthBar()
    {
        owner = GetComponentInParent<Enemy>().gameObject;
        spr = owner.GetComponent<SpriteRenderer>();
        size = spr.transform.localScale;
        hpBarBase = (GameObject)Resources.Load("Prefabs/EnemyHealthbarCanvas");
        child = hpBarBase.gameObject.transform.GetChild(0);
        hpBar = child.gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
        barPosition = new Vector2(transform.position.x, transform.position.y + 4);
        GameObject go;
        go = Instantiate(hpBarBase, barPosition, Quaternion.identity) as GameObject;
        go.name = "EnemyHealthBarCanvas";
        go.transform.localScale = size*2;// new Vector2(1, 1);
        go.transform.SetParent(owner.transform);
        //object[] gob = new object[2];
        //float f = 5000f;
        //gob[0] = hpBar.gameObject;
        //gob[1] = f;
        //owner.SendMessage("SetHealth", gob);
    }
	
}
