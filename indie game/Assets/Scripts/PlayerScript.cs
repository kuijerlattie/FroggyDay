using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : stats {

    const int qSpell = 0;
    int wSpell = 1;
    int eSpell = 2;
    int rSpell = 3;

    Image qImageSlot;
    Image wImageSlot;
    Image eImageSlot;
    Image rImageSlot;

    public enum SpellSlots
    {
        qSpell,
        wSpell,
        eSpell,
        rSpell
    }

    AttackScript attackscript;

	// Use this for initialization
	void Start () {
        maxhealth = 100;
        health = 100;
        maxmana = 100;
        mana = 100;

        qImageSlot = GameObject.Find("SpellQ").GetComponent<Image>();
        wImageSlot = GameObject.Find("SpellW").GetComponent<Image>();
        eImageSlot = GameObject.Find("SpellE").GetComponent<Image>();
        rImageSlot = GameObject.Find("SpellR").GetComponent<Image>();

        attackscript = GetComponent<AttackScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<AttackScript>().MageAttack(qSpell);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mana >= attackscript.spellmanager.spellslist[qSpell].manacost)
            {
                mana -= attackscript.spellmanager.spellslist[qSpell].manacost;
                GetComponent<AttackScript>().MageAttack(wSpell);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mana >= attackscript.spellmanager.spellslist[eSpell].manacost)
            {
                mana -= attackscript.spellmanager.spellslist[eSpell].manacost;
                GetComponent<AttackScript>().MageAttack(eSpell);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (mana >= attackscript.spellmanager.spellslist[rSpell].manacost)
            {
                mana -= attackscript.spellmanager.spellslist[rSpell].manacost;
                GetComponent<AttackScript>().MageAttack(rSpell);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

    }


    public void SetSpell(int spellid, SpellSlots spellslot)
    {
        switch (spellslot)
        {
            case SpellSlots.qSpell:
                //set a image to the hud slot
                //qImageSlot = 
                break;
            case SpellSlots.wSpell:
                wSpell = spellid;
                break;
            case SpellSlots.eSpell:
                eSpell = spellid;
                break;
            case SpellSlots.rSpell:
                rSpell = spellid;
                break;
            default:
                break;
        }
    }

    
}
