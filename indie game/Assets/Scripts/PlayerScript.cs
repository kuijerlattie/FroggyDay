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

    Image qCooldownOverlay;
    Image wCooldownOverlay;
    Image eCooldownOverlay;
    Image rCooldownOverlay;

    Image healthOverlay;
    Image manaOverlay;

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

        qCooldownOverlay = GameObject.Find("CooldownOverlayQ").GetComponent<Image>();
        wCooldownOverlay = GameObject.Find("CooldownOverlayW").GetComponent<Image>();
        eCooldownOverlay = GameObject.Find("CooldownOverlayE").GetComponent<Image>();
        rCooldownOverlay = GameObject.Find("CooldownOverlayR").GetComponent<Image>();

        healthOverlay = GameObject.Find("HealthOverlay").GetComponent<Image>();
        manaOverlay = GameObject.Find("ManaOverlay").GetComponent<Image>();

        attackscript = GetComponent<AttackScript>();

        SetSpell(SpellSlots.qSpell, 0);
        SetSpell(SpellSlots.wSpell, 1);
        SetSpell(SpellSlots.eSpell, 2);
        SetSpell(SpellSlots.rSpell, 3);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCooldowns();
        UpdateHPMana();
        UpdateInput();
    }

    void UpdateCooldowns()
    {
        qCooldownOverlay.fillAmount = GetComponent<AttackScript>().coolDowns[qSpell] / GetComponent<AttackScript>().spellmanager.spellslist[qSpell].cooldown;
        wCooldownOverlay.fillAmount = GetComponent<AttackScript>().coolDowns[wSpell] / GetComponent<AttackScript>().spellmanager.spellslist[wSpell].cooldown;
        eCooldownOverlay.fillAmount = GetComponent<AttackScript>().coolDowns[eSpell] / GetComponent<AttackScript>().spellmanager.spellslist[eSpell].cooldown;
        rCooldownOverlay.fillAmount = GetComponent<AttackScript>().coolDowns[rSpell] / GetComponent<AttackScript>().spellmanager.spellslist[rSpell].cooldown;
        healthOverlay.fillAmount = ((float)health / (float)maxhealth);
        manaOverlay.fillAmount = ((float)mana / (float)maxmana);
    }

    void UpdateHPMana()
    {

    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CastSpell(qSpell);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CastSpell(wSpell);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastSpell(eSpell);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CastSpell(rSpell);
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

    void CastSpell(int spellId)
    {
        int result = GetComponent<AttackScript>().MageAttackMouse(spellId);
        switch (result)
        {
            case -1:
                //spell failed, programmer error
                break;
            case 0:
                //spell succes
                break;
            case 1:
                //spell failed, low on mana
                break;
            case 2:
                //spell failed, still on cooldown
                break;
            default:
                Debug.Log("spell with id: " + qSpell + " returned the following unusual value: " + result);
                break;
        }
    }
}
