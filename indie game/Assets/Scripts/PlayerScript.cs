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

        SetSpell(SpellSlots.qSpell, 0);
        SetSpell(SpellSlots.wSpell, 1);
        SetSpell(SpellSlots.eSpell, 2);
        SetSpell(SpellSlots.rSpell, 3);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCooldowns();
        UpdateInput();
    }

    void UpdateCooldowns()
    {

    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<AttackScript>().MageAttackMouse(qSpell);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<AttackScript>().MageAttackMouse(wSpell);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<AttackScript>().MageAttackMouse(eSpell);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<AttackScript>().MageAttackMouse(rSpell);
        }
    }

    public void SetSpell(SpellSlots spellslot, int spellid)
    {
        switch (spellslot)
        {
            case SpellSlots.qSpell:
                //set a image to the hud slot
                qImageSlot.sprite = attackscript.spellmanager.spellslist[qSpell].icon;
                break;
            case SpellSlots.wSpell:
                wSpell = spellid;
                wImageSlot.sprite = attackscript.spellmanager.spellslist[wSpell].icon;
                break;
            case SpellSlots.eSpell:
                eImageSlot.sprite = attackscript.spellmanager.spellslist[eSpell].icon;
                eSpell = spellid;
                break;
            case SpellSlots.rSpell:
                rImageSlot.sprite = attackscript.spellmanager.spellslist[rSpell].icon;
                rSpell = spellid;
                break;
            default:
                break;
        }
    }

    
}
