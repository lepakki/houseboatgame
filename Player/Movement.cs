using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PlayerBase
{

    void Start()
    {
        //Debug.Log("NON-Virtual Start!");
        canUseLadder = false;
        rb2d = GetComponent<Rigidbody2D>();
        gravityStore = rb2d.gravityScale;

    }

    void FixedUpdate()
    {
        xAxis = Input.GetAxis("Horizontal");
        HandleLadders();
        HandleHorizontalMovement();
        HandleJumping();

    }

    void HandleHorizontalMovement()
    {
        
        rb2d.velocity = new Vector3(xAxis * stats.currentSpeed, rb2d.velocity.y, 0);
    }

    void HandleJumping()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, stats.power.CurrentVal * 1.3f);
                grounded = false;
            }
        }
    }

    void HandleLadders()
    {
        if ((canUseLadder == true))
        {
            float y = Input.GetAxis("Vertical");
            rb2d.velocity = new Vector3(xAxis * stats.currentSpeed, y * stats.currentSpeed, 0);
        }
    }

    // TODO
    // Dashing
    void StartDash()
    {

    }

    IEnumerator dashTimer()
    {
        return null;
    }

    //Potential jumping method
    /*void Jump()
    { 
        Debug.Log("Should Jump");

        Vector3 rayOrigin = GetComponent<Collider2D>().bounds.center;

        float rayDistance = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        Ray ray = new Ray();
        ray.origin = rayOrigin;
        ray.direction = Vector3.down;
        if (Physics.Raycast(ray, rayDistance, 1 << 8))
        {
            Debug.Log("Jumping");
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * stats.currentStrenght, ForceMode2D.Impulse);
        }
    }*/

    


}
