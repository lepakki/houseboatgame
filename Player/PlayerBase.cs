using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    //Reference to Stats script
    public Stats stats;
    public Stat stat;

    //Player attributes
    public float xAxis;
    public float gravityStore;

    public bool grounded;
    public bool canUseLadder;

    //Player in-game stats (maximum values)
    public float health;
    public float energy;
    public float speed;
    public float power;

    //Player components
    public Rigidbody2D rb2d;

    public virtual void OnEnable()
    {
        stats = gameObject.GetComponent<Stats>();
        health = 150;
        energy = 200;
        power = 10;
        speed = 5;

    }

    public virtual void Awake()
    {
        
        
    }



    //Collision and trigger handling
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            stats.alterHealth(-19);
        }
        if (col.gameObject.tag == "EnemyProjectile")
        {
            stats.alterHealth(-(int)col.gameObject.GetComponent<EnemyBaseProjectile>().GetDamage());
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            stats.alterHealth(-1);
        }
        if (col.gameObject.tag == "Ground") {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            canUseLadder = true;
            rb2d.gravityScale = 0;
        }
        if (col.tag == "Enemy")
        {
            stats.alterHealth(-20);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            canUseLadder = false;
            rb2d.gravityScale = gravityStore;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            stats.alterHealth(-1);
        }
    }


}
    /*
    //Player attributes
    public float health;
    public float speed;
    public float weight;
    public float gravityScale;

    //Player components
    public Rigidbody2D rb2d;
    public SpriteRenderer spr;

    //Player scripts
    Movement movementScript;

    //Player head gameobject
    public GameObject playerHead;

    //Link to Stats
    Stats stats;

    void OnEnable()
    {
        tag = "Player";
        name = "PlayerCharacter";
        spr = gameObject.AddComponent<SpriteRenderer>();
        spr.sprite = "Plane";
        rb2d = gameObject.AddComponent<Rigidbody2D>();
        movementScript = GetComponent<Movement>();
        playerHead = GameObject.Find("Player_Head");
        playerHead.transform.SetParent(gameObject.transform);
        stats = GetComponent<Stats>();
        rb2d =  GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
*/