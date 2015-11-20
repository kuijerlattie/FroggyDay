using UnityEngine;
using System.Collections;

public abstract class UsableItem : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract bool Use(stats user);
}
