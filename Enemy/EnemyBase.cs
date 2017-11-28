using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour {

    //Enemy class
    [SerializeField]
    public enum enemyClass { Dummy, Drooler, Shooter };
    public enemyClass selectedClass;

    //Reference to player
    public PlayerBase player;
    public Vector3 playerLocation;

    //Reference to healthbar
    public Image healthBar;
    public Color green;
    public Color red;
    public Color yellow;
    public Color tempColor;

    //Reference to loot for getting loot
    LootList lootlist;

    //Reference to projectiles
    Projectile projectile;
    BaseProjectile baseProjectile;

    public float startingHealth;
    public float health;
    public float speed;
    public int lootValue;
    public int expValue;
    public float damage;
    public float weight;
    public bool canMove;
    public bool canBeDamaged;
    public bool isDead;
    public bool canShoot;
    public float shootingDistance;


    //Capture locations
    public Vector3 startingLocation;
    public Vector3 currentLocation;
    public float distanceToPlayer;
    public float distanceToStart;

    //Behaviour variables
    public bool goingThisWay;
    public float maximumDistanceFromOrigin;
    public float patrolRadius;
    public state startingState;
    [SerializeField]
    public Vector3[] patrolPoints = new Vector3[2];

    [SerializeField]
    public state selectedState;
    public enum state { Patrol = 1, Stop = 2, Follow = 3, Dead = 4, Return = 5, Shoot = 6 };
    private state[] states = { state.Patrol, state.Stop, state.Follow, state.Dead, state.Return, state.Shoot };

    Rigidbody2D rb2d;

    public virtual void OnEnable()
    {
        tag = "Enemy";
        isDead = false;
        canMove = true;
        canBeDamaged = true;

        lootlist = FindObjectOfType<LootList>();
        player = FindObjectOfType<PlayerBase>();
        InitializeEnemy();
    }

    float getHealth()
    {
        return health;
    }

    public virtual state getCurrentState()
    {
        return selectedState;
    }

    public virtual void InitializeHealthBar()
    {
        
        //Ehkä tarpeeton?
        //healthBar.fillAmount = startingHealth;
    }


    public virtual void TakeDamage(float amount)
    {
        if (canBeDamaged)
        {
            healthBar.fillAmount = health / startingHealth;
            FloatingTextController.CreateFloatingText(amount.ToString(), transform);
            
            //healthBar.color = Color.Lerp(red, green, health / startingHealth);
            if(healthBar.fillAmount > 0.5f)
            {
                healthBar.color = Color.Lerp(yellow, green, health / startingHealth);
                tempColor = healthBar.color;
            } else if(healthBar.fillAmount <= 0.5f)
            {
                healthBar.color = Color.Lerp(red, tempColor, health / startingHealth);
            }

            if (((health -= amount) <= 0) && !isDead)
            {
                
                healthBar.fillAmount = 0;
                healthBar.color = green;
                SelectState(state.Dead);
                //Die();
            }
        }
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            //This will roll loot
            Invoke("_tick", 4.0f);
            //lootlist.Roll(lootValue);
            canBeDamaged = false;
            canMove = false;
            isDead = true;
            Debug.Log(gameObject.name + " has died.");
        }
   }

    public void _tick()
    {
        SetHealth(startingHealth);
        canBeDamaged = true;
        canMove = true;
        SelectState(state.Return);
        CancelInvoke();
        isDead = false;
        
    }

    // Use this for initialization
    public virtual void Start () {
        
	}

    // Update is called once per frame
    public virtual void Update () {
        checkPosition(selectedClass);
        Behave(selectedState);
    }

    public virtual void InitializeEnemy()
    {


        green = new Color(0, 1, 0);
        red = new Color(1, 0, 0);
        yellow = new Color(1, 1, 0);
        tempColor = new Color(0, 1, 0);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        goingThisWay = true;
        canShoot = false;
        maximumDistanceFromOrigin = 20;
        patrolRadius = 2;
        startingLocation = transform.position;
        setUpPatrolPoints();
        startingState = state.Patrol;
        SelectState(startingState);
        
    }

    public virtual void SetHealth(float health)
    {
        this.health = health;
        healthBar.fillAmount = health / startingHealth;
    }

    public void followPlayer()
    {
        if (selectedState == state.Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerLocation.x, transform.position.y, 0), 0.03f);
        }
    }

    public void Patrol()
    {
        if((transform.position - patrolPoints[0]).magnitude < 0.5f || (transform.position - patrolPoints[1]).magnitude < 0.5f)
        {
            goingThisWay = !goingThisWay;
        }

        if (goingThisWay)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0], 0.01f);
        }
        else if (!goingThisWay)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[1], 0.01f);
        }
    }

    void setUpPatrolPoints()
    {
        patrolPoints[0] = new Vector3(startingLocation.x + patrolRadius, startingLocation.y, 0);
        patrolPoints[1] = new Vector3(startingLocation.x - patrolRadius, startingLocation.y, 0);

    }

    public void Behave(state state)
    {

        switch (state)
        {
            case state.Patrol:
                Patrol();
                break;
            case state.Stop:
                //stopPatrolling();
                break;
            case state.Follow:
                followPlayer();
                //stopPatrolling();
                break;
            case state.Dead:
                Die();
                break;
            case state.Return:
                returnToOrigin();
                //stopPatrolling();
                break;
            case state.Shoot:
                shooting();
                break;
            default:
                break;
        }
    }

    public void returnToOrigin()
    {
        if ((distanceToStart) > 0.202f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startingLocation.x, transform.position.y, 0), 0.03f);
        }
        else
        {
            SelectState(state.Patrol);
        }
    }

    void shooting()
    {

    }

    void checkPosition(enemyClass enemyClass)
    {
        currentLocation = transform.position;
        playerLocation = player.transform.position;
        distanceToPlayer = (currentLocation - playerLocation).magnitude;
        distanceToStart = (currentLocation - startingLocation).magnitude;

        switch (enemyClass)
        {   
            case enemyClass.Dummy:
                break;
            case enemyClass.Drooler:
                if (selectedState != state.Dead)
                {
                    if (distanceToStart > maximumDistanceFromOrigin)
                    {
                        SelectState(state.Return);
                    }
                    else if (distanceToPlayer < 8 && selectedState != state.Return)
                    {
                            SelectState(state.Follow);
                    }
                }
                break;
            case enemyClass.Shooter:
                
                if (selectedState != state.Dead)
                {
                    if (distanceToStart > maximumDistanceFromOrigin || (distanceToPlayer > shootingDistance * 1.35 && selectedState == state.Shoot)) // && selectedState != state.Return && selectedState != state.Shoot
                    {
                        SelectState(state.Return);
                    }
        
                    if ((distanceToPlayer <= shootingDistance) && (distanceToPlayer < maximumDistanceFromOrigin))
                    {
                        SelectState(state.Shoot);
                    } 
                }
                break;
            default:
                break;
        }

        
    }

    void SelectState(state state)
    {
        switch (state)
        {
            case state.Patrol:
                selectedState = state.Patrol;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Stop:
                selectedState = state.Stop;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Follow:
                selectedState = state.Follow;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Dead: 
                selectedState = state.Dead;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Return:
                selectedState = state.Return;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Shoot:
                selectedState = state.Shoot;
                Debug.Log("Entered " + state + " state.");
                break;
            default:
                break;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "TestProjectile")
        //{
        //    TakeDamage((int)col.gameObject.GetComponent<Projectile>().GetDamage());
        //}
        //This is for inheritance of projectiles
        //This is the current one
        if (col.gameObject.tag == "Projectile")
        {
            TakeDamage((int)col.gameObject.GetComponent<BaseProjectile>().GetDamage());
            //Debug.Log("Color : " + healthBar.color + " Health : " + health);
        }


    }
}
