using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBase : MonoBehaviour
{

    //Reference to enemy
    Shooter shooter;

    //Reference to weapons array
    Weapons weapons;

    //Weapon parameters
    public string weaponName;
    public float projectileSpeed;
    public float projectileWeight;
    public float projectileDamage;
    public float attackSpeed;
    public float attackCost;
    public string projectileName;
    public bool projectilePiercing;

    float[] projectileSpeedRange = new float[2];
    float[] projectileWeightRange = new float[2];
    float[] projectileDamageRange = new float[2];
    float[] attackSpeedRange = new float[2];
    float[] attackCostRange = new float[2];

    public GameObject projectile;
    public GameObject enemy;
    ProjectileHolder ph_script;
    GameObject ph_obj;

    public bool attackOnCooldown;
    public Vector2 attackOrigin;

    public virtual IEnumerator coolDown(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        attackOnCooldown = false;
    }

    public virtual void Shoot()
    {
        Debug.Log("SHOOT METHOD");
        
            if (!attackOnCooldown)
            {
                GameObject go = Instantiate(projectile, attackOrigin, Quaternion.identity) as GameObject;
                go.name = projectileName;
                attackOnCooldown = true;
                StartCoroutine(coolDown(attackSpeed));
            }
        
    }

    public virtual void createWeapon(string weaponName)
    {
        this.weaponName = weaponName;
        initializeRandomizer();
        randomizeStats();

    }

    public virtual void createWeapon()
    {
        initializeRandomizer();
        randomizeStats();
    }

    public virtual void createWeapon(string weaponName, int check)
    {
        this.weaponName = weaponName;
        projectileSpeed = 0.013f;
        projectileWeight = 0f;
        projectileDamage = 15f;
        attackSpeed = 1f;
    }

    public virtual void RerollWeaponStats()
    {
        createWeapon();
    }

    void initializeRandomizer()
    {
        projectileSpeedRange[0] = 0.01f;//700f;
        projectileSpeedRange[1] = 0.02f;//1600f;

        projectileWeightRange[0] = 0;//0.001f;
        projectileWeightRange[1] = 0f;

        projectileDamageRange[0] = 8f;
        projectileDamageRange[1] = 99f;

        attackSpeedRange[0] = 0.8f;
        attackSpeedRange[1] = 1f;

        attackCostRange[0] = 2.0f;
        attackCostRange[1] = 6.0f;

    }

    void randomizeStats()
    {
        projectileSpeed = Random.Range(projectileSpeedRange[0], projectileSpeedRange[1]);
        projectileWeight = Random.Range(projectileWeightRange[0], projectileWeightRange[1]);
        projectileDamage = Random.Range(projectileDamageRange[0], projectileDamageRange[1]);
        attackSpeed = Random.Range(attackSpeedRange[0], attackSpeedRange[1]);
        attackCost = Random.Range(attackCostRange[0], attackCostRange[1]);
    }

    void OnEnable()
    {
        this.tag = "Weapon";
    }
    void Awake()
    {
        shooter = GetComponent<Shooter>();
        enemy = GameObject.Find("Shooter");
        attackOnCooldown = false;
        ph_obj = GameObject.Find("ProjectileHolder");
        ph_script = ph_obj.GetComponent<ProjectileHolder>();
        projectile = ph_script.projectiles[3];
        projectileName = projectile.name;

    }
    // Use this for initialization
    void Start()
    {
        initializeRandomizer();
        randomizeStats();
    }

    public string getStats()
    {
        return string.Format("Weapon : {0}\nProjectile : {1} \nProjectile speed : {2:000.0} \nProjectile weight : {3:0.00} \nProjectile damage : {4:0.00} \nAttack speed : {5:0.00} \nAttack cost : {6:0.00} \nProjectile piercing : {7}"
            , weaponName, projectileName, projectileSpeed, projectileWeight, projectileDamage, attackSpeed, attackCost, projectilePiercing);
    }

}
