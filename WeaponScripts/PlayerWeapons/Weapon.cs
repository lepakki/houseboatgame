using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    //Reference to weapons array
    Weapons weapons;

    //Reference to playerstats
    Stats stats;

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
    public GameObject player;
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
        if (gameObject.transform.parent.tag == "Player")
        {
            if (!attackOnCooldown && (stats.energy.CurrentVal > attackCost))
            {
                GameObject go = Instantiate(projectile, attackOrigin, Quaternion.identity) as GameObject;
                stats.alterEnergy(-attackCost);
                go.name = projectileName;
                //go.transform.rotation = Quaternion.LookRotation(go.GetComponent<Rigidbody2D>().velocity);
                attackOnCooldown = true;
                StartCoroutine(coolDown(attackSpeed));
            }
        }
    }

    public virtual void createWeapon(string weaponName)
    {
        this.weaponName = weaponName;
        //projectilePiercing = projectile.;
        initializeRandomizer();
        randomizeStats();
        
    }

    public virtual void createWeapon()
    {
        //this.weaponName = weaponName;
        //projectilePiercing = projectile.;
        initializeRandomizer();
        randomizeStats();
    }

    public virtual void RerollWeaponStats()
    {
        createWeapon();
    }

    void initializeRandomizer()
    {
        projectileSpeedRange[0] = 0.06f;//700f;
        projectileSpeedRange[1] = 0.45f;//1600f;

        projectileWeightRange[0] = 0;//0.001f;
        projectileWeightRange[1] = 0f;

        projectileDamageRange[0] = 8f;
        projectileDamageRange[1] = 99f;

        attackSpeedRange[0] = 0.003f;
        attackSpeedRange[1] = 0.3f;

        attackCostRange[0] = 1.0f;
        attackCostRange[1] = 5.0f;

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
        stats = FindObjectOfType<Movement>().GetComponent<Stats>();
        player = GameObject.Find("PH_Top");
        attackOnCooldown = false;
        ph_obj = GameObject.Find("ProjectileHolder");
        ph_script = ph_obj.GetComponent<ProjectileHolder>();
        projectile = ph_script.projectiles[4]; //2 for normal bullet
        projectileName = projectile.name;

    }
    // Use this for initialization
    void Start () {
        //stats = FindObjectOfType<Movement>().GetComponent<Stats>();
        initializeRandomizer();
        randomizeStats();
	}

    public string getStats()
    {
        return string.Format("Weapon : {0}\nProjectile : {1} \nProjectile speed : {2:0.00} \nProjectile weight : {3:0.00} \nProjectile damage : {4:0.00} \nAttack speed : {5:0.00} \nAttack cost : {6:0.00}"
            ,weaponName ,projectileName, projectileSpeed, projectileWeight, projectileDamage, attackSpeed, attackCost);
    }

}
