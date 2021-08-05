using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public void LoadMenuScreen(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}

	public void LoadGameScreen(){
		SceneManager.LoadScene(2);
	}

	public void LoadCreditsScreen(){
		SceneManager.LoadScene(4);
	}

	public void QuitGame(){
		Application.Quit();
	}

}
