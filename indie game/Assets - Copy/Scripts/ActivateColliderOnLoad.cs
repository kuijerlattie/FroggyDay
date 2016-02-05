using UnityEngine;
using System.Collections;

public class ActivateColliderOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
