using UnityEngine;
using System.Collections;
using System;

public class Manapotion : Item{

    [HideInInspector]
    public int manaValue;

    public Manapotion()
    {
        model = GameObject.FindObjectOfType<ItemManager>().manapotionModel;
    }



    public override void Pickup()
    {   
        Spells spellmanager = GameObject.FindObjectOfType<Spells>();
        spellmanager.spellslist[7].uses++;
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
