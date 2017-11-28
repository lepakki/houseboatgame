using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour
{

    public Weapon weapon;
    public Rigidbody2D rb2d;
    public SpriteRenderer spr;
    public float projectileVelocity;
    public float projectileDamage;
    public float projectileDamageRNG;
    public float projectileMinimumDamage;
    public float projectileMaximumDamage;
    public bool projectilePiercing;

    public virtual void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        spr.sortingLayerName = "Player";
        this.tag = "Projectile";
        weapon = GameObject.Find("PH_Weapon").GetComponent<Weapon>();
        initializeProjectileStats();
    }

    // Use this for initialization
    public virtual void Start()
    {

        if (weapon == null)
        {
            Debug.Log("gun is NULL");
        }

        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        rb2d.AddForce(dir * projectileVelocity);
        //gameObject.transform.rotation = Quaternion.LookRotation(dir * projectileVelocity);
        Destroy(gameObject, 1.5f);
        //StartCoroutine(destroyAfter(1.5f));
    }

    public void initializeProjectileStats()
    {
        rb2d.mass = weapon.projectileWeight;
        projectileVelocity = weapon.projectileSpeed;
        projectileDamage = weapon.projectileDamage;
        projectilePiercing = weapon.projectilePiercing;

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
        Destroy(gameObject, 0.19f);
        
        if (col.gameObject.tag == "Enemy")
        {
        }
    }

    IEnumerator destroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
