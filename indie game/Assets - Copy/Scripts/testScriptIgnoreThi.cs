﻿using UnityEngine;
using System.Collections;

public class testScriptIgnoreThi : MonoBehaviour {

	//rotation
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime, 0);
	}
}
