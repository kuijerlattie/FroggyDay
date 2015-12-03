using UnityEngine;
using System.Collections;

public class ActivateWaveScript : MonoBehaviour {

    public int AreaNumber = 1;

    EnemyManager manager;
    bool active = true;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            Debug.Log("new wave started");
            manager.StartWave(AreaNumber);
            active = false;
        }
    }

    public void Reset()
    {
        active = true;
    }
}
