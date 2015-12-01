using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : stats {

    bool fireSpecialISActive = false;
    bool iceSpecialIsActive = false;
    bool airSpecialIsActive = false;
    GameObject HUD;
    GameObject pauseMenu;

    int spellQ = 0;
    int spellW = 0;
    int spellE = 0;
    int spellR = 0;
    int spell1 = 6;
    int spell2 = 7;
    int spell3 = 0;

    public int spellFireNormal = 0;
    public int spellIceNormal = 0;
    public int spellAirNormal = 0;
    public int spellFireSpecial = 0;
    public int spellIceSpecial = 0;
    public int spellAirSpecial = 0;

    public int spellHealthPotion = 6;
    public int spellManaPotion = 7;
    public int spellAbilityRun = 0;

    Image imageSlotQ;
    Image imageSlotW;
    Image imageSlotE;
    Image imageSlotR;
    Image imageSlot1;
    Image imageSlot2;
    Image imageSlot3;

    Image cooldownOverlayQ;
    Image cooldownOverlayW;
    Image cooldownOverlayE;
    Image cooldownOverlayR;
    Image cooldownOverlay1;
    Image cooldownOverlay2;
    Image cooldownOverlay3;

    Text itemCounter1;
    Text itemCounter2;

    Image healthOverlay;
    Image manaOverlay;

    Text goldText;

    public enum SpellSlots
    {
        spellQ,
        spellW,
        spellE,
        spellR,
        spell1,
        spell2,
        spell3
    }

    AttackScript attackscript;

	// Use this for initialization
	void Start () {

        maxhealth = 100;
        health = 10;
        maxmana = 100;
        mana = 100;

        imageSlotQ = GameObject.Find("SpellQ").GetComponent<Image>();
        imageSlotW = GameObject.Find("SpellW").GetComponent<Image>();
        imageSlotE = GameObject.Find("SpellE").GetComponent<Image>();
        imageSlotR = GameObject.Find("SpellR").GetComponent<Image>();
        imageSlot1 = GameObject.Find("Spell1").GetComponent<Image>();
        imageSlot2 = GameObject.Find("Spell2").GetComponent<Image>();
        imageSlot3 = GameObject.Find("Spell3").GetComponent<Image>();

        cooldownOverlayQ = GameObject.Find("CooldownOverlayQ").GetComponent<Image>();
        cooldownOverlayW = GameObject.Find("CooldownOverlayW").GetComponent<Image>();
        cooldownOverlayE = GameObject.Find("CooldownOverlayE").GetComponent<Image>();
        cooldownOverlayR = GameObject.Find("CooldownOverlayR").GetComponent<Image>();
        cooldownOverlay1 = GameObject.Find("CooldownOverlay1").GetComponent<Image>();
        cooldownOverlay2 = GameObject.Find("CooldownOverlay2").GetComponent<Image>();
        cooldownOverlay3 = GameObject.Find("CooldownOverlay3").GetComponent<Image>();

        itemCounter1 = GameObject.Find("ItemCounter1").GetComponent<Text>();
        itemCounter2 = GameObject.Find("ItemCounter2").GetComponent<Text>();

        healthOverlay = GameObject.Find("HealthOverlay").GetComponent<Image>();
        manaOverlay = GameObject.Find("ManaOverlay").GetComponent<Image>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();

        attackscript = GetComponent<AttackScript>();

        SetSpell(SpellSlots.spellQ, spellQ);
        SetSpell(SpellSlots.spellW, spellFireNormal);
        SetSpell(SpellSlots.spellE, spellIceNormal);
        SetSpell(SpellSlots.spellR, spellAirNormal);
        SetSpell(SpellSlots.spell1, spellHealthPotion);
        SetSpell(SpellSlots.spell2, spellManaPotion);
        SetSpell(SpellSlots.spell3, spellAbilityRun);

        HUD = GameObject.Find("HUD");
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        Debug.Log("spell = " + attackscript.spellmanager.spellslist[6].name);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0) //this loop stops when game is paused
        {
            UpdateHud();
            UpdateInput();
        }

        mana += 10 * Time.deltaTime;

        /// Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPause();
        }
    }

    void UpdateHud()
    {
        cooldownOverlayQ.fillAmount = GetComponent<AttackScript>().coolDowns[spellQ] / GetComponent<AttackScript>().spellmanager.spellslist[spellQ].cooldown;
        cooldownOverlayW.fillAmount = GetComponent<AttackScript>().coolDowns[spellW] / GetComponent<AttackScript>().spellmanager.spellslist[spellW].cooldown;
        cooldownOverlayE.fillAmount = GetComponent<AttackScript>().coolDowns[spellE] / GetComponent<AttackScript>().spellmanager.spellslist[spellE].cooldown;
        cooldownOverlayR.fillAmount = GetComponent<AttackScript>().coolDowns[spellR] / GetComponent<AttackScript>().spellmanager.spellslist[spellR].cooldown;
        cooldownOverlay1.fillAmount = GetComponent<AttackScript>().coolDowns[spell1] / GetComponent<AttackScript>().spellmanager.spellslist[spell1].cooldown;
        cooldownOverlay2.fillAmount = GetComponent<AttackScript>().coolDowns[spell2] / GetComponent<AttackScript>().spellmanager.spellslist[spell2].cooldown;
        cooldownOverlay3.fillAmount = GetComponent<AttackScript>().coolDowns[spell3] / GetComponent<AttackScript>().spellmanager.spellslist[spell3].cooldown;
        healthOverlay.fillAmount = ((float)health / (float)maxhealth);
        manaOverlay.fillAmount = ((float)mana / (float)maxmana);
        goldText.text = gold.ToString();
        itemCounter1.text = GetComponent<AttackScript>().spellmanager.spellslist[spell1].uses.ToString();
        itemCounter2.text = GetComponent<AttackScript>().spellmanager.spellslist[spell2].uses.ToString();
    }

    void UpdateInput()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetSpell(SpellSlots.spellW, spellFireSpecial);
            SetSpell(SpellSlots.spellE, spellIceSpecial);
            SetSpell(SpellSlots.spellR, spellAirSpecial);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SetSpell(SpellSlots.spellW, spellFireNormal);
            SetSpell(SpellSlots.spellE, spellIceNormal);
            SetSpell(SpellSlots.spellR, spellAirNormal);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CastSpell(spellQ);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CastSpell(spellW);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastSpell(spellE);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CastSpell(spellR);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CastSpell(spell1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CastSpell(spell2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CastSpell(spell3);
        }
    }

    public void CheckPause()
    {
        if (Time.timeScale == 1)//pause game
        {
            Time.timeScale = 0;
            HUD.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else //unpause game
        {
            Time.timeScale = 1;
            HUD.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    public void SetSpell(SpellSlots spellslot, int spellid)
    {
        switch (spellslot)
        {
            case SpellSlots.spellQ:
                //set a image to the hud slot
                spellQ = spellid;
                imageSlotQ.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                break;
            case SpellSlots.spellW:
                spellW = spellid;
                imageSlotW.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("w set to " + spellid);
                break;
            case SpellSlots.spellE:
                spellE = spellid;
                imageSlotE.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("e set to " + spellid);
                break;
            case SpellSlots.spellR:
                spellR = spellid;
                imageSlotR.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("r set to " + spellid);
                break;
            case SpellSlots.spell1:
                spell1 = spellid;
                imageSlot1.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("1 set to " + spellid);
                break;
            case SpellSlots.spell2:
                spell2 = spellid;
                imageSlot2.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("2 set to " + spellid);
                break;
            case SpellSlots.spell3:
                spell3 = spellid;
                imageSlot3.sprite = attackscript.spellmanager.spellslist[spellid].icon;
                Debug.Log("3 set to " + spellid);
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
            case 3:
                //spell not learned yet
                break;
            case 4:
                //spell out of charges
                break;
            default:
                Debug.Log("spell with id: " + spellQ + " returned the following unusual value: " + result);
                break;
        }
    }

    public void LootGold(int g)
    {
        gold += g;
    }

    public void QuitToMenu()
    {
        Application.LoadLevel(0);
    }

    public void enableQspell()
    {
        spellQ = 1;
    }
}
