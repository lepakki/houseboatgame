using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private float attackCost;
    [SerializeField]
    private float attackCooldown;

    public GameObject item;
    //public Transform firePoint;
    public Vector3 attackOrigin;
    private bool attackOnCooldown;
    
    Stats stats;

	// Use this for initialization
	void Start () {
        stats = FindObjectOfType<Movement>().GetComponent<Stats>();
        attackCooldown = 0.5f;
        attackCost = 5.0f;
        attackOnCooldown = false;
	}

    public void setShootingPower(float coolDown, float cost)
    {
        attackCooldown = coolDown;
        attackCost = cost;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        attackOrigin = transform.position;

        //float mWheel = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            stats.alterPower(1);
            //Debug.Log("up");
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            stats.alterPower(-1);
            //Debug.Log("down");
        }

            if ((Input.GetMouseButton(0)) && (!attackOnCooldown) && (stats.energy.CurrentVal > attackCost))
        {
            stats.alterEnergy(-attackCost);
            Instantiate(item, attackOrigin, Quaternion.identity);    
            attackOnCooldown = true;
            StartCoroutine(coolDown(attackCooldown));

            Debug.Log("SHOOTED!");
        }
	}

    IEnumerator coolDown(float attackCooldown)
    {
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
}
