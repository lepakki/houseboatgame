using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Enemy {

    public GameObject world;
    public Vector3 mousePos;
    public bool goingThisWay;
    public bool grounded;
    public float patrolRadius;
    Rigidbody2D rb2d;
    NoiseTest nt;

	// Use this for initialization
	void Start () {
        world = GameObject.Find("World");
        patrolRadius = 5.5f;
        goingThisWay = true;
        grounded = false;
        rb2d = GetComponent<Rigidbody2D>();
        nt = FindObjectOfType<NoiseTest>();
	}
	
	// Update is called once per frame
	void Update () {
        float hz = Input.GetAxisRaw("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Flip();
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(grounded)
                rb2d.velocity = new Vector3(hz, 8, 0);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            int mouseX = Mathf.RoundToInt(mousePos.x);
            int mouseY = Mathf.RoundToInt(mousePos.y);
            nt.DestroyAt(mouseX,mouseY);
        }

        rb2d.velocity = new Vector3(hz * 2, rb2d.velocity.y, 0);

        //Patrol();
	}

    void Patrol()
    {
        if (goingThisWay)
        {
            rb2d.velocity = new Vector2(-1.3f, rb2d.velocity.y);
        } else if (!goingThisWay)
        {
            rb2d.velocity = new Vector2(1.3f, rb2d.velocity.y);
        }
    }

    void Flip()
    {
        goingThisWay = !goingThisWay;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        if (grounded)
            rb2d.velocity = new Vector2(rb2d.velocity.x, 5);
            //transform.position = new Vector2(transform.position.x, transform.position.y + 1.1f);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
    /*
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            Debug.Log("Jump now");
            Jump();

        }
    }*/
}
