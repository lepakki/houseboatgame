using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    PlayerBase playerBase;

    [SerializeField]
    public Stat health;
    [SerializeField]
    public Stat energy;
    [SerializeField]
    public Stat power;
    [SerializeField]
    private Stat speed;

    //Experience
    [SerializeField]
    public Stat experience;

    //Get health, energy & power from playerbase
    public float playerMaxHealth;
    public float playerMaxEnergy;
    public float playerMaxPower;

    public float currentHealth { get; set; }
    public float currentEnergy { get; set; }
    public float currentSpeed { get; set; }
    public float currentPower { get; set; }

    private float energyTemp;
    private float powerTemp;

    void OnEnable ()
    {
        playerBase = FindObjectOfType<PlayerBase>();

        playerMaxHealth = playerBase.health;
        playerMaxEnergy = playerBase.energy;
        playerMaxPower = playerBase.power;
        health.MaxVal = playerMaxHealth;
        energy.MaxVal = playerMaxEnergy;
        power.MaxVal = playerMaxPower;
        currentHealth = PlayerPrefs.GetFloat("health", 50f);
        currentEnergy = PlayerPrefs.GetFloat("energy", 50f);
        currentPower = 10;//PlayerPrefs.GetFloat("power", 10f); //Playerprefs do not work on webgl
        currentSpeed = playerBase.speed;
    }

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();

        InitializeBars();
    }
	
	// Update is called once per frame
	void Update () {
        regenerateEnergy(0.2f); // Was 0.1f
        regenerateHealth(0.013f);
	}

    public void InitializeBars()
    {
        health.Initialize(playerMaxHealth, currentHealth);
        energy.Initialize(playerMaxEnergy, currentEnergy);
        power.Initialize(playerMaxPower, currentPower);
    }

    /*void OnDestroy()
    {
        PlayerPrefs.SetFloat("health", health.CurrentVal);
        PlayerPrefs.SetFloat("energy", energy.CurrentVal);
        PlayerPrefs.SetFloat("power", power.CurrentVal);

        Debug.Log("SaveGame " + "Health = " + health.CurrentVal + '\n' + "Energy = " + energy.CurrentVal + '\n' + "Power = " + power.CurrentVal);
    }*/

    public void regenerateEnergy(float amount)
    {
        energy.CurrentVal += amount;
    }

    public void regenerateHealth(float amount)
    {
        health.CurrentVal += amount;
    }

    public void alterHealth(float amount)
    {
        health.CurrentVal += amount;
    }

    public void alterEnergy(float amount)
    {
        energyTemp = energy.CurrentVal;
        if ((energyTemp += amount) > 0.0f)
        {
            energy.CurrentVal += amount;
        }
    }

    public void alterPower(float amount)
    {
        powerTemp = power.CurrentVal;
        if ((powerTemp += amount) > 0.0f)
        {
            power.CurrentVal += amount;
        }
    }
}
