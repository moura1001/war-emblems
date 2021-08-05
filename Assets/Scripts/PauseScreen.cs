using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

	public static bool gameIsPaused = false;

	public GameObject pauseMenuUI;
	public GameObject userUI;
	
	// Update is called once per frame
	void Update(){

		if(Input.GetKeyDown(KeyCode.Escape)){

			if(gameIsPaused){
				Resume();
			
			} else{
				Pause();
			}

		}
		
	}

	public void Resume(){

		pauseMenuUI.SetActive(false);
		userUI.SetActive(true);
		Time.timeScale = 1f;
		gameIsPaused = false;

	}

	void Pause(){

		pauseMenuUI.SetActive(true);
		userUI.SetActive(false);
		Time.timeScale = 0f;
		gameIsPaused = true;

	}

}
