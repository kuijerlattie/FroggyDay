using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] spawnlocations;
    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject rangedEnemyPrefab;
    float timer = 0;

    public int[] areaCounters;

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
        spawnlocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
        Debug.Log("spawn locations found: " + spawnlocations.Length);
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;
        enemyCount++;
        spawnCount++;
    }

    void SpawnRangedEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(rangedEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
        enemy.GetComponent<EnemyBase>().isWaveEnemy = true;

        enemyCount++;
        spawnCount++;
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 location = spawnlocations[Random.Range(0, spawnlocations.Length)].transform.position;
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
        enemyCount--;
    }

    public void AddToArea(int areanumber)
    {
        areaCounters[areanumber - 1] += 1;
    }

    public void RemoveFromArea(int areanumber)
    {
        areaCounters[areanumber - 1] -= 1;
    }

    public bool IsAreaEmpty(int areanumber)
    {
        if (areaCounters[areanumber - 1] <= 0)
        {
            return true;
        }
        return false;
    }

    public int GetEnemyCountInArea(int areanumber)
    {
        return areaCounters[areanumber - 1];
    }
}
