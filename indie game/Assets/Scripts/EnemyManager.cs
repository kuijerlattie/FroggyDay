using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] spawnlocations;
    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject player;
    float timer = 0;

    bool waveActive = false;
    bool spawnEnemies = false;

    public int waveLevel = 0;
    public int waveSize = 0; //amount of enemies in current wave;
    public float waveDelay = 5.0f; //time between waves;

    public int spawnCount = 0; //enemies spawned this round;
    public int enemyCount = 0; //enemies still alive
    private float spawnDelay = 1f;



	// Use this for initialization
	void Start () {
        spawnlocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemyCount++;
        spawnCount++;
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 location = spawnlocations[Random.Range(0, 2)].transform.position;
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
                }
            }
        }
        else //wave not active
        {
            if (timer >= waveDelay)
            {
                StartNewWave();
                timer = 0;
            }
        }

        if (spawnEnemies)
        {
            if (timer >= spawnDelay)
            {
                SpawnEnemy();
                timer = 0;
            }
        }
        timer += Time.deltaTime;
        Debug.Log(timer);
	}

    void StartNewWave()
    {
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

        Debug.Log("enemies left: " + enemyCount);
    }
}
