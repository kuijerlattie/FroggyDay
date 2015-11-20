using UnityEngine;
using System.Collections;

public class Healingpotion : UsableItem {

    stats playerstats;
    [HideInInspector]
    public int healingValue;
	// Use this for initialization
	void Start () {
        playerstats = GameObject.Find("Player").GetComponent<stats>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool Use(stats user)
    {
        if (playerstats.health < playerstats.maxhealth)
        {
            user.health += healingValue;
            return true;
        }
        return false;
    }
}
