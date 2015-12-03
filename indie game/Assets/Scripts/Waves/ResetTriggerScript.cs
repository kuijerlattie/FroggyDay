using UnityEngine;
using System.Collections;

public class ResetTriggerScript : MonoBehaviour {

    public GameObject waveActivator;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("wavestarter reset!");
        waveActivator.GetComponent<ActivateWaveScript>().Reset();
    }
}
