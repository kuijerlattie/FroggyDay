using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {


    List<List<GameObject>> spawnlocations = new List<List<GameObject>>();
    List<GameObject> spawnlocations1 = new List<GameObject>();
    List<GameObject> spawnlocations2 = new List<GameObject>();
    List<GameObject> spawnlocations3 = new List<GameObject>();
    List<GameObject> spawnlocations4 = new List<GameObject>();
    List<GameObject> spawnlocations5 = new List<GameObject>();
    List<GameObject> spawnlocations6 = new List<GameObject>();

    int CurrentArea;

    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject rangedEnemyPrefab;
    float timer = 0;

    bool waveActive = false;
    bool spawnEnemies = false;

    [HideInInspector]
    public int waveLevel = 0;
    [HideInInspector]
    public int waveSize = 0; //amount of enemies in current wave;
    public float waveDelay = 5.0f; //time between waves;

    [HideInInspector]
    public int spawnCount = 0; //enemies spawned this round;
    [HideInInspector]
    public int enemyCount = 0; //enemies still alive
    private float spawnDelay = 1f;



	// Use this for initialization
	void Start () {
        spawnlocations1.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn1"));
        spawnlocations2.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn2"));
        spawnlocations3.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn3"));
        spawnlocations4.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn4"));
        spawnlocations5.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn5"));
        spawnlocations6.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawn6"));

        spawnlocations.Add(spawnlocations1);
        spawnlocations.Add(spawnlocations2);
        spawnlocations.Add(spawnlocations3);
        spawnlocations.Add(spawnlocations4);
        spawnlocations.Add(spawnlocations5);
        spawnlocations.Add(spawnlocations6);
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;
        enemy.GetComponent<EnemyBase>().area = CurrentArea;
        enemyCount++;
        spawnCount++;
    }

    void SpawnRangedEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(rangedEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;
        enemy.GetComponent<EnemyBase>().area = CurrentArea;

        enemyCount++;
        spawnCount++;
    }

    Vector3 GetSpawnLocation()
    {
        Debug.Log("total spawnlocations in area" + spawnlocations[CurrentArea - 1].Count);
        Vector3 location = spawnlocations[CurrentArea-1][Random.Range(0, spawnlocations[CurrentArea-1].Count)].transform.position;
        return location;
    }
	
	// Update is called once per frame
	void Update () {
        if (waveActive)
        {
            if (spawnCount >= waveSize)
            {
                spawnEnemies = false;

                if (enemyCount <= 0)
                {
                    waveActive = false;
                    timer = 0;
                }
            }
        }
        //else //wave not active
        //{
        //    if (timer >= waveDelay)  //not using this system anymore, as waves get spawned (and despawned) with triggers in the map
        //    {
        //        StartNewWave();
        //        timer = 0;
        //    }
        //}

        if (spawnEnemies)
        {
            if (timer >= spawnDelay)
            {
                int i = Random.Range(1, 3);
                switch (i)
                {
                    case 1:
                        SpawnEnemy();
                        break;
                    case 2:
                        SpawnRangedEnemy();
                        break;
                    default:
                        SpawnEnemy();
                        break;
                }
                timer = 0;
            }
        }
        timer += Time.deltaTime;
	}

    void StartNewWave()
    {
        Debug.Log("starting wave " + (waveLevel + 1) + " in area " + CurrentArea);
        waveLevel++;
        waveActive = true;
        spawnEnemies = true;

        waveSize = waveLevel * 5;
        enemyCount = 0;
        spawnCount = 0;

        //soundeffect? hudupdate?
    }

    public void RemoveEnemy()
    {
        enemyCount--;
    }

    public void StartWave(int area = 1)
    {
        CurrentArea = area;
        if (!waveActive)
            StartNewWave();
    }

    public int EnemiesLeft()
    {
        return enemyCount;
    }
}
