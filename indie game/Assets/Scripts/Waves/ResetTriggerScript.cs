using UnityEngine;
using System.Collections;

public class ResetTriggerScript : MonoBehaviour {

    public GameObject waveActivator;
    Collider[] colliders;
    float radius = 5;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider col in colliders)
        {
           
            if (col.gameObject.layer == 9)
            {
                waveActivator.GetComponent<ActivateWaveScript>().Reset();
            }
        }
	}
}
