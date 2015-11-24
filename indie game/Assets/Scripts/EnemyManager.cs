using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] spawnlocations;
    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject rangedEnemyPrefab;
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
        Debug.Log("spawn locations found: " + spawnlocations.Length);
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemyCount++;
        spawnCount++;
    }

    void SpawnRangedEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(rangedEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemyCount++;
        spawnCount++;
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 location = spawnlocations[Random.Range(0, spawnlocations.Length-1)].transform.position;
        return location;
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(enemyCount);

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
        Debug.Log("enemy died");
        enemyCount--;
    }
}
