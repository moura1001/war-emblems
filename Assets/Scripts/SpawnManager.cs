using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager: MonoBehaviour{

    private EnemySpawner[] enemySpawners;
    [System.NonSerialized]
    public EnemySpawner enemySpawnerScript;

    private Action spawnerCallback;
    private float spawnCD;

    private IEnumerator spawnTimeManager;
    public int timeToStart;
    public int timeToNextHorde;
    private int timeUIUpdate;
    
    public Text timeText;

    private void SetSpawnCallback(Action spawnerCallback, float spawnCD){
        this.spawnerCallback = spawnerCallback;
        this.spawnCD = spawnCD;
    }

    // Start is called before the first frame update
    void Awake(){
        GameObject enemySpawnersGO = GameObject.Find("Enemy Spawners");

        timeUIUpdate = timeToNextHorde;
        int size = enemySpawnersGO.transform.childCount;

        enemySpawners = new EnemySpawner[size];
        for(int i = 0; i < size; i++)
            enemySpawners[i] = enemySpawnersGO.transform.GetChild(i).GetComponent<EnemySpawner>();

        enemySpawnerScript = enemySpawners[0];

        SetSpawnCallback(enemySpawners[0].SpawnEnemy, enemySpawners[0].spawnCD);

        UnityEngine.Random.InitState(UnityEngine.Random.Range(int.MinValue, int.MaxValue));

        if(timeText != null)
            //timeManagerCallback = MainGameTimeManager;
            StartCoroutine(MainGameTimeManager());
        else
            //timeManagerCallback = HomecreenTimeManager;
            StartCoroutine(HomecreenTimeManager());

    }

    public void NextHordeSelect(){

        int sortWave = UnityEngine.Random.Range(0, 101);
        //sortWave = (char) 64;
        Debug.Log(sortWave);

        if(sortWave <= 50){
            enemySpawnerScript = enemySpawners[0];
            SetSpawnCallback(enemySpawners[0].SpawnEnemy, enemySpawners[0].spawnCD);
        }
        else{
            enemySpawnerScript = enemySpawners[1];
            SetSpawnCallback(enemySpawners[1].SpawnEnemy, enemySpawners[1].spawnCD);
        }

        enemySpawnerScript.didSpawn = true;

    }

    private bool DidSpawn(){
        return enemySpawnerScript.didSpawn;
    }

    private IEnumerator SpawnTimeManager(){

        while(DidSpawn()){
            spawnerCallback();
            //spawnCDremaing = 0f;
            //timeUIUpdate = timeToNextHorde;
            yield return new WaitForSeconds(spawnCD);
            //StartCoroutine(SpawnTimeManager());
        }

        yield return null;

    }

    private IEnumerator MainGameTimeManager(){

        while(timeToStart >= 0){
            timeText.text = "Time to start: " + timeToStart-- + "s";
            yield return new WaitForSeconds(1f);
        }

        spawnTimeManager = SpawnTimeManager();

        while(true){
            timeText.text = "Spawning";
            yield return StartCoroutine(spawnTimeManager);
            StopCoroutine(spawnTimeManager);
            
            while(timeUIUpdate >= 0){
                timeText.text = "Time until next horde: " + timeUIUpdate-- + "s";
                yield return new WaitForSeconds(1f);
            }

            timeUIUpdate = timeToNextHorde;
            NextHordeSelect();
            spawnTimeManager = SpawnTimeManager();
            yield return null;
        }

    }

    private IEnumerator HomecreenTimeManager(){

        while(timeToStart >= 0){
            timeToStart--;
            yield return new WaitForSeconds(1f);
        }

        spawnTimeManager = SpawnTimeManager();

        while(true){
            yield return StartCoroutine(spawnTimeManager);
            StopCoroutine(spawnTimeManager);

            while(timeUIUpdate >= 0){
                timeUIUpdate--;
                yield return new WaitForSeconds(1f);
            }

            timeUIUpdate = timeToNextHorde;
            NextHordeSelect();
            spawnTimeManager = SpawnTimeManager();
            yield return null;
        }

    }

}
