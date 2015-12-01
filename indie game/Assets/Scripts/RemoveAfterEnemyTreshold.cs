using UnityEngine;
using System.Collections;

public class RemoveAfterEnemyTreshold : MonoBehaviour {

    public int area;
    public int EnemiesLeft;
    public bool RemoveWhenAreaIsCleared = false;
    public bool RemoveAfterWaves = false;
    public int waves;
    EnemyManager manager;
	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (RemoveWhenAreaIsCleared)
        {
            if (manager.IsAreaEmpty(area))
                Destroy(gameObject);
        }
        else if(RemoveAfterWaves)
        {
            if (GameObject.FindObjectOfType<EnemyManager>().waveLevel > waves)
                Destroy(gameObject);
        }
        else if (manager.GetEnemyCountInArea(area) <= EnemiesLeft)
            Destroy(gameObject);
        
	}
}
