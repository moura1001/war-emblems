using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int lives = 20;
	public int money = 100;

	public Text moneyText;
	public Text livesText;

	void Start()
	{
		moneyText.text = "Money: $" + money;
		livesText.text = "Lives: " + lives;
	}

	public void LoseLife(int l = 1){
		lives -= l;
		this.UIUpdate();
		if(lives <= 0){
			GameOver();
		}
	}

	public void GameOver(){
		Debug.Log("Game Over!");
		SceneManager.LoadScene(3);
	}

	public void UIUpdate(){
		moneyText.text = "Money: $" + money;
		livesText.text = "Lives: " + lives;
	}

}
