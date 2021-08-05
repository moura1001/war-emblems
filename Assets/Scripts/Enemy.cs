using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
	ELITE, NORMAL
}

public class Enemy : MonoBehaviour {

	private Vector3 targetPathNode;
	
	public float speedValue;
	private float speed;
	public float healthValue;
	private float health;

	public int moneyValue;
	private int money;

	private int level;

	[SerializeField]
	private EnemyType enemyType;

	private bool die;
	public bool lastSpawned;

	private Action reachedGoal;

	// Use this for initialization
	void Awake(){

		die = true;
		lastSpawned = false;

		level = MyGameManager.Instance.spawnManagerScript.enemySpawnerScript.levelHorde;

		speed = speedValue;
		health = healthValue;
		money = moneyValue;

		SetLevel();

		if (MyGameManager.Instance.homeScreenScript == null)
        {
			targetPathNode = MyGameManager.Instance.pathGO.transform.GetChild(0).position;
			reachedGoal = MainGameReachedGoal;

		} 
		else
        {
			targetPathNode = HomeScreenTarget.GetTarget(enemyType);
			reachedGoal = HomeScreenReachedGoal;
		}
	}

	public void SetNextPathNode(Transform targetPathNode)
	{
		if(targetPathNode != null)
        {
			// We reached the node
			this.targetPathNode = targetPathNode.position;
			return;

		}

		reachedGoal();
	}

	// Update is called once per frame
	void Update(){

		Vector3 dir = targetPathNode - transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		// TODO: Consider ways to smooth this motion.
		// Move towards node
		transform.Translate(dir.normalized * distThisFrame, Space.World);
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*5);
		
	}

	void MainGameReachedGoal()
	{
		MyGameManager.Instance.RemoveEnemy(gameObject);
		MyGameManager.Instance.scoreManagerScript.LoseLife();
		Destroy(gameObject);
	}

	void HomeScreenReachedGoal()
	{
		Destroy(gameObject);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		Debug.Log("Hit!");
		if (health <= 0 && die)
		{
			die = false;
			Die();
		}
	}

	void Die()
	{
		// TODO: Do this more safely!
		MyGameManager.Instance.RemoveEnemy(gameObject);
		MyGameManager.Instance.scoreManagerScript.money += money;
		MyGameManager.Instance.scoreManagerScript.UIUpdate();
		Destroy(gameObject);

	}

	private void SetLevel()
	{

		if (level == 0)
			return;

		speed += level;
		health += (20 * level);
		money *= (2 * level);

	}

	public float GetSpeed()
	{
		return speed;
	}

	public void DecreaseSpeed(float speed)
	{

		float decreasedSpeed = this.speed * (1 - speed);

		if (decreasedSpeed > 1)
			this.speed = decreasedSpeed;
		else
			this.speed = 1;
	}

	public float GetHealth()
	{
		return health;
	}

	public void DecreaseHealth(float health)
	{

		float decreasedHealth = this.health * (1 - health);

		if (decreasedHealth > 1)
			this.health = decreasedHealth;
		else
			this.health = 1;
	}

	public int GetMoney()
	{
		return money;
	}

	public void SetMoney(int money)
	{
		this.money = money;
	}
}
