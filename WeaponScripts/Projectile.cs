using UnityEngine;
using System.Collections;
//using System;

public class Projectile : MonoBehaviour
{

    W_new w_new;
    Rigidbody2D rb2d;
    float projectileVelocity;
    float projectileDamage;
    float projectileDamageRNG;
    float projectileMinimumDamage;
    float projectileMaximumDamage;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Use this for initialization
    void Start()
    {

        w_new = GameObject.Find("thisweapon").GetComponent<W_new>();
        initializeProjectileStats();
        if (w_new == null)
        {
            Debug.Log("gun is NULL");
        }
        
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        rb2d.AddForce(dir * projectileVelocity);
        StartCoroutine(destroyAfter(1.5f));
    }

    void initializeProjectileStats()
    {
        rb2d.mass = w_new.projectileWeight;
        projectileVelocity = w_new.projectileSpeed;
        projectileDamage = w_new.projectileDamage;

        projectileMinimumDamage = (projectileDamage - projectileDamage * 0.25f);
        projectileMaximumDamage = (projectileDamage + projectileDamage * 0.25f);
    }

    public float GetDamage()
    {
        projectileDamageRNG = Random.Range(projectileMinimumDamage, projectileMaximumDamage);
        return projectileDamageRNG;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
        }
    }

    IEnumerator destroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
