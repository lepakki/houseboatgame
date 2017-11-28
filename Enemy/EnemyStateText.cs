using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateText : MonoBehaviour {


    public GameObject parent;
    EnemyBase eb;
    Text text;


	// Use this for initialization
	void Start () {
        //eb = FindObjectOfType<EnemyBehaviour>();
        
        text = GetComponent<Text>();
        parent = transform.parent.gameObject.transform.parent.gameObject;//enemy = GameObject.Find("PH_EnemyWithBehaviour");// GameObject.FindGameObjectWithTag("Enemy");
        eb = parent.GetComponent<EnemyBase>();

    }
	
	// Update is called once per frame
	void Update () {
        text.text = "[State: " + eb.selectedState.ToString() + "]";
        gameObject.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + 1, 0);
    }
}
