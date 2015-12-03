using UnityEngine;
using System.Collections;

public class ActivateWaveScript : MonoBehaviour {

    public int AreaNumber = 1;

    EnemyManager manager;
    bool active = true;

    Collider[] colliders;
    float radius = 5;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            //this code replaces a trigger, since triggers refuse to work with the navmeshagent            
            colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.layer == 9)
                {
                    manager.StartWave(AreaNumber);
                    active = false;
                }
            }
        }
	}

    public void Reset()
    {
        active = true;
    }
}
