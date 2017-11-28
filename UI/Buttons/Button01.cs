using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Button01 : MonoBehaviour {

    PlayerAttack playerAttack;
    public Button[] button;
    public bool enemy01active, enemy02active, helpActive, maybeActive;
    public GameObject enemy01, enemy02;
    public GameObject helpPanel;
    public GameObject weaponPanel;
    public GameObject[] enemies;
    public GameObject enemyParent;

    void Awake()
    {
        button = FindObjectsOfType<Button>();  
        helpPanel = GameObject.Find("HelpPanel");
        weaponPanel = GameObject.Find("WeaponStatsPanel");
        enemyParent = GameObject.Find("Enemies");
        enemies = new GameObject[enemyParent.transform.childCount];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemyParent.transform.GetChild(i).gameObject;
        }
        
        
    }

	// Use this for initialization
	void Start () {
        maybeActive = true;
        enemy01active = false;
        enemy01 = GameObject.Find("PH_Enemy");
        //enemy01.SetActive(enemy01active);
        //enemy01 = GameObject.Find("PH_EnemyWithBehaviour");
        
        helpPanel.SetActive(false);
        weaponPanel.SetActive(false);
        helpActive = false;
        

        button[0].onClick.AddListener(toggleHelpPanel);
        //button[0].onClick.AddListener(toggleEnemies);
        /*
        button[5].onClick.AddListener(setPowerOne);
        button[4].onClick.AddListener(setPowerTwo);
        button[3].onClick.AddListener(setPowerThree);
        button[2].onClick.AddListener(setPowerFour);
        button[1].onClick.AddListener(toggleEnemy01);
        button[0].onClick.AddListener(toggleEnemy02);
        */
        
        //button[3].onClick.AddListener(setPowerOne);
        //button[2].onClick.AddListener(setPowerTwo);
        //button[1].onClick.AddListener(setPowerThree);
        //button[0].onClick.AddListener(setPowerFour);
        
        //playerAttack = FindObjectOfType<PlayerAttack>();
	}

    void toggleEnemies()
    {
        maybeActive = !maybeActive;
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(maybeActive);
        }
    }

    void toggleHelpPanel()
    {
        helpActive = !helpActive;
        helpPanel.SetActive(helpActive);
        weaponPanel.SetActive(helpActive);

    }

    void toggleEnemy01()
    {
        enemy02 = GameObject.Find("PH_Enemy");
        enemy01active = !enemy01active;
        enemy01.SetActive(enemy01active);
    }

    void toggleEnemy02()
    {
        Debug.Log("BUTTON 2");
        enemy02 = GameObject.Find("PH_Enemy");
        //GameObject enemy02 = GameObject.Find("PH_EnemyWithBehaviour");
        enemy02active = !enemy02active;
        enemy02.SetActive(enemy02active);
    }

    void setPowerOne()
    {
        playerAttack.setShootingPower(0.05f, 1.0f);
        Debug.Log("Player attack changed to : 0.05f 1.0f");
    }
    void setPowerTwo()
    {
        playerAttack.setShootingPower(0.1f, 3.0f);
        Debug.Log("Player attack changed to : 0.1f 3.0f");
    }
    void setPowerThree()
    {
        playerAttack.setShootingPower(0.3f, 7.0f);
        Debug.Log("Player attack changed to : 0.3f 7.0f");
    }
    void setPowerFour()
    {
        playerAttack.setShootingPower(0.5f, 15.0f);
        Debug.Log("Player attack changed to : 0.5f 15.0f");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
