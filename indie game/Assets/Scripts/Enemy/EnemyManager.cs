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

    [HideInInspector]
    public int CurrentArea = 1;

    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject rangedEnemyPrefab;
    float timer = 0;

    bool waveActive = false;
    bool spawnEnemies = false;
    public bool waveDone = false;

    [HideInInspector]
    public int waveLevel = 0;
    public float waveDelay = 5.0f; //time between waves;

    [HideInInspector]
    public float spawnCredits;
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
        spawnCredits -= 1f;
    }

    void SpawnStrongEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;
        enemy.GetComponent<EnemyBase>().area = CurrentArea;
        enemyCount++;
        spawnCredits -= 2.5f;
    }

    void SpawnRangedEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(rangedEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;
        enemy.GetComponent<EnemyBase>().area = CurrentArea;

        enemyCount++;
        spawnCredits -= 1.5f;
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
            if (spawnCredits < 1)
            {
                spawnEnemies = false;

                if (enemyCount <= 0)
                {
                    waveDone = true;
                    waveActive = false;
                    timer = 0;
                }
            }
        }

        if (spawnEnemies)
        {
            if (timer >= spawnDelay)
            {
                float i = Random.Range(0, 1);
                if (i <= 0.6) //60% chance
                {
                    SpawnEnemy();
                }
                else if (i <= 0.9) // 30% chance
                {
                    if (waveLevel > 1)
                        SpawnRangedEnemy();
                }
                else if (i <= 1) // 60% chance
                {
                    if (waveLevel > 4)
                        SpawnStrongEnemy();
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
        waveDone = false;

        enemyCount = 0;
        spawnCredits = 3f * Mathf.Pow(1.3f, waveLevel);

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
