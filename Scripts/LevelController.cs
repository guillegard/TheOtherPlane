using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	[SerializeField]
    public static int killedEnemies = 0;
    public static int coins = 0;
    public delegate void KE();
    public static event KE EnemyDies;
    public delegate void PS();
    public static event PS PickSpirit;

    public GameObject pauseUI;
    public PauseScript pauseScript;
    public PlayerController playerController;

    public static void KillEnemy()
    {
        killedEnemies++;
        if(EnemyDies != null)
        {
            EnemyDies();
        }
    }

    public static void PickUpSpirit()
    {
       if(PickSpirit != null)
        {
            PickSpirit();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            if (!pauseUI.activeSelf)
            {
                Time.timeScale = 0;
                pauseScript.playerState = playerController.enabled;
                playerController.enabled = false;
                pauseUI.SetActive(true);
            }
        }
    }
}
