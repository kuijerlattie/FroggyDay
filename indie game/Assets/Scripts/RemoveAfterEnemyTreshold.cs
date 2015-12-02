using UnityEngine;
using System.Collections;

public class RemoveAfterEnemyTreshold : MonoBehaviour {
    public bool RemoveAfterWaves = false;
    public int waves;
    EnemyManager manager;
	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if(RemoveAfterWaves)
        {
            if (GameObject.FindObjectOfType<EnemyManager>().waveLevel >= waves && GameObject.FindObjectOfType<EnemyManager>().EnemiesLeft() == 0)
                Destroy(gameObject);
        }
	}
}
