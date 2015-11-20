using UnityEngine;
using System.Collections;

public class Manapotion : UsableItem{

    [HideInInspector]
    public int manaValue;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public override bool Use(stats user)
    {
        if (user.mana < user.maxmana)
        {
            user.mana += manaValue;
            return true;
        }
        return false;

    }
}
