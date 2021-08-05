using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float spawnCD = 1.5f;

	[System.Serializable]
	public class WaveComponent{
		public GameObject enemyPrefab;
		public short num;
		[System.NonSerialized]
		public short spawned = 0;
	}

	public WaveComponent[] waveComps;

	public int levelHorde;

	[System.NonSerialized]
	public bool didSpawn;

	[System.NonSerialized]
	public int enemysSpawned;

	private GameObject lastSpawned;

	// Use this for initialization
	void Start(){
		didSpawn = true;
		enemysSpawned = 0;
		foreach(WaveComponent wc in waveComps)
			enemysSpawned += wc.num;

		lastSpawned = null;
	}

	public void SpawnEnemy(){

		didSpawn = false;

		// Go thought the wave comps until we find something to spawn
		foreach (WaveComponent wc in waveComps){
			if (wc.spawned < wc.num){
				// Spawn it!

				wc.spawned++;

				lastSpawned = Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);
				MyGameManager.Instance.AddEnemy(lastSpawned);

				didSpawn = true;
				break;
			}
		}

		if (!didSpawn){
			// Wave must be complete!
			lastSpawned.GetComponent<Enemy>().lastSpawned = true;

			//TODO: Instantiate next wave object!

			// That was the last wave -- what do we want to do?
			// What if instead of DESTROYING wave objects,
			// we just made them inactive, and then when we run
			// out of waves, we restart at the first one,
			// but double all enemy HPs or something?

			levelHorde++;

			//enemysSpawned = 0;
			foreach (WaveComponent wc in waveComps){
				wc.spawned = 0;
				//enemysSpawned += wc.num;
			}
			
		}

	}
}
