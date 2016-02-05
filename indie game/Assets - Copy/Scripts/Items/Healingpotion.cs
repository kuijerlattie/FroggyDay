using UnityEngine;
using System.Collections;
using System;

public class Healingpotion : Item {

    stats playerstats;
    [HideInInspector]
    public int healingValue;
	// Use this for initialization
	void Start () {
        playerstats = GameObject.Find("Player").GetComponent<stats>();  //How can this work??? it is not even monobehaviour????????????????
        
    }

    public Healingpotion()
    {
        model = GameObject.FindObjectOfType<ItemManager>().healthpotionModel;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override void Pickup()
    {
        Spells spellmanager = GameObject.FindObjectOfType<Spells>();
        spellmanager.spellslist[6].uses++;
       
    }

    public override bool Use(stats user)
    {
        playerstats = user;
        if (playerstats.health < playerstats.maxhealth)
        {
            user.health += healingValue;
            return true;
        }
        return false;
    }
}
