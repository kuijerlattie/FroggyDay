using UnityEngine;
using System.Collections;
using System;

public class EquipableItem : Item {

	// Use this for initialization
	void Start () {
	
	}

    public override void Pickup()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override bool Use(stats user)
    {
        return false;    
    }
}
