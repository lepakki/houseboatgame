using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    //Attributes //Maybe a bool for returning?
    public float enemySpeed;
    public bool goingThisWay;
    public bool startedPatrolling;
    public float maximumDistanceFromOrigin;
    public state startingState;

    //Capture starting location
    Vector3 startingLocation;

    //Components

    Rigidbody2D rb2d;
    BoxCollider2D bxc;
    DrawRayToPlayer drtp;

    //Patrol points
    public Transform[] patrolPoints;

    public enum state {Patrolling = 1, Stopped = 2, Following = 3, Dead = 4, Returning = 5};
    state[] states = { state.Patrolling, state.Stopped, state.Following, state.Dead, state.Returning };

    [SerializeField]
    public state selectedState;
    

	// Use this for initialization
	void Start () {
        startingLocation = transform.position;
        maximumDistanceFromOrigin = 20f;
        patrolPoints = GetComponentsInChildren<Transform>();
        setUpPatrolPoints();
        startedPatrolling = false;
        goingThisWay = false;
        drtp = GetComponent<DrawRayToPlayer>();
        enemySpeed = 3;
        gameObject.tag = "Enemy";
        rb2d = gameObject.AddComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        //SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();
        //spr.sprite = Sprite.FindObjectOfType(xhair);
        bxc = gameObject.AddComponent<BoxCollider2D>();
        StartCoroutine(alterStates());
        
    }
	
	// Update is called once per frame
	void Update () {
        checkPosition();
        Behave(selectedState);
	}

    void checkPosition()
    {
        if ((transform.position - startingLocation).magnitude > maximumDistanceFromOrigin)
        {
            SelectState(state.Returning);
        }
        if ((transform.position - drtp.playerLocation).magnitude < 8)
        {
            SelectState(state.Following);
        }
    }

    void setUpPatrolPoints()
    {
        patrolPoints[1].position = new Vector3(gameObject.transform.position.x + 10, 0);
        patrolPoints[2].position = new Vector3(gameObject.transform.position.x - 10, 0);
        
    }

    public void Behave(state state)
    {

        switch (state)
        {
            case state.Patrolling:
                Patrol();
                startedPatrolling = true;
                break;
            case state.Stopped:
                stopPatrolling();
                break;
            case state.Following:
                followPlayer();
                stopPatrolling();
                break;
            case state.Dead:
                stopPatrolling();
                break;
            case state.Returning:
                returnToOrigin();
                stopPatrolling();
                break;
            default:
                break;
        }
    }

    public void returnToOrigin()
    {
        if ((transform.position - startingLocation).magnitude > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingLocation, 0.03f);
        } else
        {
            SelectState(state.Patrolling);
        }
    }

    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(drtp.playerLocation.x, 0, 0), 0.03f);
    }

    public void stopPatrolling()
    {
        if (startedPatrolling)
        {
            CancelInvoke("_turnAround");
            Debug.Log("Stopped a patrol.");
            startedPatrolling = false;
        }
    }

    public void Patrol()
    {
        if (goingThisWay)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[1].position, 0.01f);
        }
        else if (!goingThisWay)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[2].position, 0.01f);
        }

        if (!startedPatrolling)
        {
            Debug.Log("Started a patrol.");
            InvokeRepeating("_turnAround", 0, 3f);
        }
    }

    public void _turnAround()
    {
        goingThisWay = !goingThisWay;
    }

    public IEnumerator alterStates()
    {
        SelectState((state)states.GetValue(Random.Range(0,3)));
        yield return new WaitForSeconds(7);
        //StartCoroutine(alterStates());
    }

    void SelectState(state state)
    {
        switch (state)
        {
            case state.Patrolling:
                selectedState = state.Patrolling;    
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Stopped:
                selectedState = state.Stopped;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Following:
                selectedState = state.Following;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Dead:
                selectedState = state.Dead;
                Debug.Log("Entered " + state + " state.");
                break;
            case state.Returning:
                selectedState = state.Returning;
                Debug.Log("Entered " + state + " state.");
                break;
            default:
                break;
        }
    }
}
