using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    //Reference to player
    public PlayerBase player;

    //Reference to healthbar
    public Image healthBar;
    public Color green;
    public Color red;
    public Color yellow;

    //Reference to loot for getting loot
    LootList lootlist;

    //Reference to projectiles
    Projectile projectile;
    BaseProjectile baseProjectile;

    public float startingHealth;
    public float health;
    public float speed;
    public int lootValue;
    public float damage;
    public float weight;
    public bool canMove;
    public bool isDead;
    public bool canBeDamaged;

    void OnEnable()
    {

        this.tag = "Enemy";
        isDead = false;
        canMove = true;
        canBeDamaged = true;
        lootlist = FindObjectOfType<LootList>();
        player = FindObjectOfType<PlayerBase>();


    }

    public void InitializeHealthBar()
    {
        green = new Color(0, 1, 0);
        red = new Color(1, 0, 0);
        yellow = new Color(1, 1, 0);
        healthBar.fillAmount = startingHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (canBeDamaged)
        {
            FloatingTextController.CreateFloatingText(amount.ToString(), transform);
            healthBar.fillAmount = health / startingHealth;

            if (healthBar.fillAmount > 0.6f)
            {
                healthBar.color = Color.Lerp(yellow, green, health / startingHealth);
            }
            else if (healthBar.fillAmount <= 0.6f)
            {
                healthBar.color = Color.Lerp(red, yellow, health / startingHealth);
            }

            if (((health -= amount) <= 0) && !isDead)
            {
                healthBar.fillAmount = 0;
                healthBar.color = green;

                Die();
            }
        }
    }


    public virtual void Die()
    {
        //This will roll loot
        lootlist.Roll(lootValue);
        canBeDamaged = false;
        isDead = true;
        canMove = false;
        Invoke("_tick", 4.0f);
        Debug.Log(gameObject.name + " has died.");
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "TestProjectile")
        {
            TakeDamage((int)col.gameObject.GetComponent<Projectile>().GetDamage());
        }
        //This is for inheritance of projectiles
        //This is the current one
        if (col.gameObject.tag == "Projectile")
        {
            TakeDamage((int)col.gameObject.GetComponent<BaseProjectile>().GetDamage());
        } else
        {
            //Destroy(col.gameObject);
        }


    }
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
       if (col.tag == "Ground")
        {
            Debug.Log("Jump now");
            
        }
    }

    public virtual void SetHealth(float health)
    {
        this.health = health;
        healthBar.fillAmount = health / startingHealth;
    }

    public void _tick()
    {
        canBeDamaged = true;
        canMove = true;
        SetHealth(startingHealth);
        isDead = false;
    }

}