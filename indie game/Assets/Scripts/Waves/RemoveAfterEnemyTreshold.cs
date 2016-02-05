using UnityEngine;
using System.Collections;

public class RemoveAfterEnemyTreshold : MonoBehaviour {
    public bool RemoveAfterWaves = false;
    public int waves;
    public bool RepeatForEveryWave = true;
    EnemyManager manager;
	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if(RemoveAfterWaves)
        {
            if (manager.waveLevel >= waves && manager.waveDone)
            {
                GetComponent<NavMeshObstacle>().enabled = false;
                //GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (!manager.waveDone)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<NavMeshObstacle>().enabled = true;
            //GetComponent<MeshRenderer>().enabled = true;
        }
	}
}
