using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour {

	public GameObject pathGO;
	public GameObject targetsGO;
	public GameObject interactableGO;
	public GameObject towersPanelUI;
	public GameObject flamethrowerPanelUI;

	public ArrayList enemies;

	public SpawnManager spawnManagerScript;
	public BuildingManager buildingManagerScript;
	public ScoreManager scoreManagerScript;
	public Explosion explosionScript;
	public HomeScreenInput homeScreenScript;

	// Bit shift the index of the layer (9) to get a bit mask
	//public const int enemyLayerMask = 1 << 9;
	// Bit shift the index of the layer (10) to get a bit mask
	public const int towerLayerMask = 1 << 10;
	// Bit shift the index of the layer (11) to get a bit mask
	public const int towerSpotLayerMask = 1 << 11;


	private Action hasEnemiesListeners;
	private Action hasNotEnemiesListeners;

	private static MyGameManager _instance;
	public static MyGameManager Instance { get { return _instance; } }

	// Use this for initialization
	void Awake(){

		_instance = this;

		homeScreenScript = gameObject.GetComponent<HomeScreenInput>();
		enemies = new ArrayList();

		if (homeScreenScript == null)
		{
			scoreManagerScript = gameObject.GetComponent<ScoreManager>();
			buildingManagerScript = gameObject.GetComponent<BuildingManager>();
			explosionScript = gameObject.GetComponent<Explosion>();
		}

		spawnManagerScript = gameObject.GetComponent<SpawnManager>();

		//Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
		Physics.IgnoreLayerCollision(0, 8);

	}

	public void AddHasEnemiesListener(Action action)
	{
		hasEnemiesListeners += action;
	}

	public void AddHasNotEnemiesListener(Action action)
	{
		hasNotEnemiesListeners += action;
	}

	public void AddEnemy(GameObject enemy)
	{

		enemies.Add(enemy);

		if (hasEnemiesListeners != null)
			hasEnemiesListeners();

	}

	public void RemoveEnemy(GameObject enemy){

		enemies.Remove(enemy);

		if (enemies.Count <= 0 && hasNotEnemiesListeners != null)
			hasNotEnemiesListeners();

	}

	public void TowerUpgrade(int attributeUpgrade)
    {
		ITowerUpgrade towerUpgrade = interactableGO.GetComponent<ITowerUpgrade>();
		towerUpgrade.UpgradeRequest(attributeUpgrade);
    }
}
