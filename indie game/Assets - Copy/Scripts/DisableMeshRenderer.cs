﻿using UnityEngine;
using System.Collections;

public class DisableMeshRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}