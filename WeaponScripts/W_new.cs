using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_new : MonoBehaviour {

    //Get Stats for altering energy etc.
    Stats stats;

    ProjectileHolder ph_script;
    GameObject ph_obj;

    float[] projectileSpeedRange = new float[2];
    float[] projectileWeightRange = new float[2];
    float[] projectileDamageRange = new float[2];
    float[] attackSpeedRange = new float[2];
    float[] attackCostRange = new float[2];

    [SerializeField]
    public string name;
    [SerializeField]
    public float projectileSpeed;
    [SerializeField]
    public float projectileWeight;
    [SerializeField]
    public float projectileDamage;
    [SerializeField]
    public float attackSpeed;
    [SerializeField]
    public float attackCost;
    [SerializeField]
    public GameObject projectile;

    public string projectileName;
    public GameObject player;
    public Vector3 attackOrigin;
    public bool attackOnCooldown;

    void Awake()
    {
        stats = FindObjectOfType<Movement>().GetComponent<Stats>();
        player = GameObject.Find("PH_Top");
        attackOnCooldown = false;
        ph_obj = GameObject.Find("ProjectileHolder");
        ph_script = ph_obj.GetComponent<ProjectileHolder>();
        projectile = ph_script.projectiles[0];
        projectileName = projectile.name;
        //Debug.Log(projectile);
    }

	// Use this for initialization
	void Start () {
        name = "xaxaxa";
        initializeRandomizer();
        randomizeStats();
       
	}

    void initializeRandomizer()
    {
        projectileSpeedRange[0] = 400f;
        projectileSpeedRange[1] = 2200f;

        projectileWeightRange[0] = 0.001f;
        projectileWeightRange[1] = 2.0f;

        projectileDamageRange[0] = 5f;
        projectileDamageRange[1] = 55f;

        attackSpeedRange[0] = 0.03f;
        attackSpeedRange[1] = 0.9f;

        attackCostRange[0] = 0.8f;
        attackCostRange[1] = 14f;

    }

    void randomizeStats()
    {
        projectileSpeed = Random.Range(projectileSpeedRange[0], projectileSpeedRange[1]);
        projectileWeight = Random.Range(projectileWeightRange[0], projectileWeightRange[1]);
        projectileDamage = Random.Range(projectileDamageRange[0], projectileDamageRange[1]);
        attackSpeed = Random.Range(attackSpeedRange[0], attackSpeedRange[1]);
        attackCost = Random.Range(attackCostRange[0], attackCostRange[1]);
    }

    IEnumerator coolDown(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        attackOnCooldown = false;
    }

    // Update is called once per frame
    void Update () {

        attackOrigin = player.transform.position;

        if (Input.GetKey(KeyCode.G) && !attackOnCooldown && (stats.energy.CurrentVal > attackCost))
        {
            GameObject go = Instantiate(projectile, attackOrigin, Quaternion.identity) as GameObject;
            stats.alterEnergy(-attackCost);
            go.name = projectileName;
            attackOnCooldown = true;
            StartCoroutine(coolDown(attackSpeed));
        }
        /*
        if (Input.GetKey(KeyCode.F) && !attackOnCooldown)
        {
            //Instantiate(projectile, attackOrigin, Quaternion.identity);
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            attackOnCooldown = true;
            StartCoroutine(coolDown(attackSpeed));
        }
        */
	}
}
