using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy {

    Rigidbody2D rb2d;
    private Vector3 startingPoint;
    private Vector3 playerLocation;
    private GameObject playerCharacter;
    public Vector3 knockBack;
    float time;


    //for instantiate
    private Quaternion qtr;
    private Vector3 tps;
    private GUIText enemiesMurderedText;
    private int enemiesMurdered;

	// Use this for initialization
	void Start () {
        Initialize();
        gameObject.name = "PH_Enemy";
        gameObject.transform.localScale *= 2;
        canMove = true;
        time = 1f;

	}

    void Initialize()
    {
        //Set stats
        health = 250;
        startingHealth = health;
        InitializeHealthBar();
        speed = 1.3f;
        weight = 2f;
        lootValue = 10;

        //GetComponents
        qtr = GetComponent<BoxCollider2D>().transform.rotation;
        tps = new Vector3(transform.position.x, transform.position.y + 25, 0);
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        //rb2d = GetComponent<Rigidbody2D>();
        //startingPoint = rb2d.position;
        transform.localScale = transform.localScale * 1.3f;

    }

	void Update () {
        //Debug.Log(player.stats.health.CurrentVal);
        if(player.stats.health.CurrentVal < 3)
        {
            Die();

        }
        playerLocation = playerCharacter.transform.position;
        checkPosition();
        moveTowardsPlayer(playerLocation);

	}

    

    void checkPosition()
    {
        if(time > 1f)
        {
            time = 0.0f;
        }
        if((transform.position - playerCharacter.transform.position).magnitude < 5)
        {
            time += 0.5f * Time.deltaTime;
            speed = Mathf.Lerp(1.3f, 5f, time);
        } else
        {
            time = 0.0f;
            speed = 1.3f;
        }
    } 

    
    //Flying enemy movement pattern
    void moveTowardsPlayer(Vector3 playerLocation)
    {
        if (canMove)
        {
            rb2d.velocity = new Vector3((playerLocation.x - transform.position.x) * speed, (playerLocation.y - transform.position.y) * speed, 0);
            drawRayCast2D(playerLocation);
        }
    }


    Vector3 drawRayCast2D(Vector3 playerLocation)
    {
        Vector3 point1 = new Vector3(playerLocation.x, playerLocation.y, 0);
        Vector3 point2 = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 diff = point1 - point2;
        //RaycastHit2D ray = Physics2D.Raycast(point2, diff);
        Debug.DrawRay(point2, diff, Color.cyan);
        return knockBack = (point2 - diff);
        

    }

    void OnDestroy()
    {
        //Instantiate(gameObject, tps, qtr);
    }
    /* // This code was for flying enemy knockback / death. OnDestroy added to enemy.cs and here above afterwards
    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerProjectile") || (col.tag == "TestProjectile"))
        {
            //enemiesMurdered++;
            //PlayerPrefs.SetInt("enemiesMurdered", enemiesMurdered);
            //enemiesMurderedText.text = "Enemies murdered : " + enemiesMurdered.ToString();
            
            Instantiate(gameObject, tps, qtr);
            
            Destroy(gameObject);
        }
        if(col.tag == "Player")
        {
            TakeDamage(55);
            canMove = false;
            Vector3 direction = transform.position - col.transform.position;
            direction.y += 1.5f;
            direction.Normalize();
            rb2d.AddRelativeForce(direction * 9, ForceMode2D.Impulse);
            time = 1f;
            Invoke("_tick", time);
            //Debug.Log(direction);
        }
    }*/

    /*
    public void TakeDamage(int amount)
    {
        FloatingTextController.CreateFloatingText(amount.ToString(), transform);
    }*/
}
