using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public GameObject pauseUI;
    public PlayerController playerController;
    public bool playerState;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Continue()
    {
        if (pauseUI.activeSelf)
        {
            Time.timeScale = 1;
            
            playerController.enabled = playerState;
            pauseUI.SetActive(false);
        }
    }

    public void Save()
    {

    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
