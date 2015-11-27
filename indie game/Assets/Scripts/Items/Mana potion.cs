using UnityEngine;
using System.Collections;
using System;

public class Manapotion : UsableItem{

    [HideInInspector]
    public int manaValue;



    public override void Pickup()
    {

        // throw new NotImplementedException();
        Spells spellmanager = GameObject.FindObjectOfType<Spells>();
        spellmanager.spellslist[6].uses++;
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
