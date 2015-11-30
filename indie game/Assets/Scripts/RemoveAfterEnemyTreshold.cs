using UnityEngine;
using System.Collections;

public class RemoveAfterEnemyTreshold : MonoBehaviour {

    public int area;
    public int EnemiesLeft;
    public bool RemoveWhenAreaIsCleared;
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
        else if (manager.GetEnemyCountInArea(area) <= EnemiesLeft)
            Destroy(gameObject);
        
	}
}
