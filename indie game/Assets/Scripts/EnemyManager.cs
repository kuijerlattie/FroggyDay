using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] spawnlocations;
    [SerializeField]
    GameObject meleeEnemyPrefab;
    [SerializeField]
    GameObject player;
    float timer = 0;
	// Use this for initialization
	void Start () {
        spawnlocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = GetSpawnLocation();
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 location = spawnlocations[Random.Range(0, 2)].transform.position;
        return location;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            SpawnEnemy();
            timer = 0;
        }
	}
}
