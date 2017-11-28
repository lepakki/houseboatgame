using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour {

    public GameObject meleeAttackTrigger;
    public Vector2 attackOrigin;
    bool meleeActive;

	// Use this for initialization
	void Start () {
        //meleeAttackTrigger = GameObject.FindGameObjectWithTag("PH_MeleeAttackTrigger");
        meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = false;
        meleeAttackTrigger.GetComponent<BoxCollider2D>().enabled = false;
        meleeActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        attackOrigin = transform.position;
        Ray2D attack = new Ray2D();
        if (meleeActive)//Input.GetMouseButtonDown(1))
        {
            if (!meleeActive) {
                attack.direction = Input.mousePosition;
                meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = true;
                meleeAttackTrigger.GetComponent<BoxCollider2D>().enabled = true;
                meleeActive = true;
                Debug.Log("ATTACK 2");
            } else
            {
                meleeAttackTrigger.GetComponent<SpriteRenderer>().enabled = false;
                meleeAttackTrigger.GetComponent<BoxCollider2D>().enabled = false;
                meleeActive = false;
            }
            
        }
	}
}
