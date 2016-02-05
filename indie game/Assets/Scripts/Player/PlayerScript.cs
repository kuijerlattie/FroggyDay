using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : stats {

    GameObject HUD;
    GameObject pauseMenu;

    public int spellQ = 0;
    public int spellW = 0;
    public int spellE = 0;
    public int spellR = 0;
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
    //Image healthOverlay2;   //this is the one attached to the player
    Image manaOverlay;

    Text goldText;
    public int Chests = 10;

    EnemyManager enemyManager;

    List<List<GameObject>> playerSpawnList = new List<List<GameObject>>();
    List<GameObject> playerspawn1 = new List<GameObject>();
    List<GameObject> playerspawn2 = new List<GameObject>();
    List<GameObject> playerspawn3 = new List<GameObject>();
    List<GameObject> playerspawn4 = new List<GameObject>();
    List<GameObject> playerspawn5 = new List<GameObject>();
    List<GameObject> playerspawn6 = new List<GameObject>();

    float deathtimer = 0;
    public bool alive = true;

    #region scrolls
    public bool qscroll = false;
    public bool wscroll = false;
    public bool escroll = false;
    public bool rscroll = false;
    #endregion

    GameObject lowhpPlayer = null;
    GameObject lowmpPlayer = null;


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
        health = 100;
        maxmana = 100;
        mana = 100;
        gold = 100; //TODO remove this in the end

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
        //healthOverlay2 = GameObject.Find("HealthOverlay2").GetComponent<Image>();
        manaOverlay = GameObject.Find("ManaOverlay").GetComponent<Image>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();

        //shitload of adds to make sure player spawns at wanted locations when he dies
        playerspawn1.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn1"));
        playerspawn2.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn2"));
        playerspawn3.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn3"));
        playerspawn4.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn4"));
        playerspawn5.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn5"));
        playerspawn6.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn6"));
        playerSpawnList.Add(playerspawn1);
        playerSpawnList.Add(playerspawn2);
        playerSpawnList.Add(playerspawn3);
        playerSpawnList.Add(playerspawn4);
        playerSpawnList.Add(playerspawn5);
        playerSpawnList.Add(playerspawn6);

        attackscript = GetComponent<AttackScript>();

        SetSpell(SpellSlots.spellQ, spellQ);
        SetSpell(SpellSlots.spellW, spellFireNormal);
        SetSpell(SpellSlots.spellE, spellIceNormal);
        SetSpell(SpellSlots.spellR, spellAirNormal);
        SetSpell(SpellSlots.spell1, spellHealthPotion);
        SetSpell(SpellSlots.spell2, spellManaPotion);
        SetSpell(SpellSlots.spell3, spellAbilityRun);


        enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        HUD = GameObject.Find("HUD");
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        Debug.Log("spell = " + attackscript.spellmanager.spellslist[6].name);
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInChildren<Animator>().GetBool("attack1"))
        {
            GetComponentInChildren<Animator>().SetBool("attack1", false);
        }
        if (GetComponentInChildren<Animator>().GetBool("attack2"))
        {
            GetComponentInChildren<Animator>().SetBool("attack2", false);
        }
        if (GetComponentInChildren<Animator>().GetBool("potion"))
        {
            GetComponentInChildren<Animator>().SetBool("potion", false);
        }

        if(health < 33 && lowhpPlayer == null)
        {

            lowhpPlayer = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.LowHP).gameObject;
            GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.WatchHP);
        }
        else
        {
            if(lowhpPlayer != null && health >= 33)
            {
                lowhpPlayer.GetComponent<FmodPlayScript>().StopSoundLooped();
                lowhpPlayer = null;
            }
        }

        if (mana < 33 && lowmpPlayer == null)
        {

            lowhpPlayer = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.LowMana).gameObject;
        }
        else
        {
            if (lowmpPlayer != null && mana >= 33)
            {
                lowmpPlayer.GetComponent<FmodPlayScript>().StopSoundLooped();
                lowmpPlayer = null;
            }
        }

        if (Time.timeScale != 0) //this loop stops when game is paused
        {
            UpdateHud();
            if (alive)
                UpdateInput();
        }
        mana += 1 * Time.deltaTime;

        /// Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPause();
        }

        if (!alive)
        {
            if (deathtimer > 0)
            {
                deathtimer -= Time.deltaTime;
            }
            else
            {
                Respawn();
            }
        }
    }

    void UpdateHud()
    {
        cooldownOverlayQ.fillAmount = attackscript.coolDowns[spellQ] / attackscript.spellmanager.spellslist[spellQ].cooldown;
        cooldownOverlayW.fillAmount = attackscript.coolDowns[spellW] / attackscript.spellmanager.spellslist[spellW].cooldown;
        cooldownOverlayE.fillAmount = attackscript.coolDowns[spellE] / attackscript.spellmanager.spellslist[spellE].cooldown;
        cooldownOverlayR.fillAmount = attackscript.coolDowns[spellR] / attackscript.spellmanager.spellslist[spellR].cooldown;
        cooldownOverlay1.fillAmount = attackscript.coolDowns[spell1] / attackscript.spellmanager.spellslist[spell1].cooldown;
        cooldownOverlay2.fillAmount = attackscript.coolDowns[spell2] / attackscript.spellmanager.spellslist[spell2].cooldown;
        cooldownOverlay3.fillAmount = attackscript.coolDowns[spell3] / attackscript.spellmanager.spellslist[spell3].cooldown;
        //healthOverlay.fillAmount = ((float)health / (float)maxhealth);
        //healthOverlay2.fillAmount = healthOverlay.fillAmount;
        manaOverlay.fillAmount = ((float)mana / (float)maxmana);
        goldText.text = gold.ToString();
        itemCounter1.text = attackscript.spellmanager.spellslist[spell1].uses.ToString();
        itemCounter2.text = attackscript.spellmanager.spellslist[spell2].uses.ToString();
    }

    void RefreshSpells() { }

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
            GetComponentInChildren<Animator>().SetBool("attack1", true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CastSpell(spellW);
            GetComponentInChildren<Animator>().SetBool("attack2", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastSpell(spellE);
            GetComponentInChildren<Animator>().SetBool("attack1", true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CastSpell(spellR);
            GetComponentInChildren<Animator>().SetBool("attack2", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CastSpell(spell1);
            GetComponentInChildren<Animator>().SetBool("potion", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CastSpell(spell2);
            GetComponentInChildren<Animator>().SetBool("potion", true);
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
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.PickUpCoin);
    }

    public void QuitToMenu()
    {
        Application.LoadLevel(0);
    }

    public void enableQspell()
    {
        spellQ = 1;
    }

    public override void Die()
    {
        if (alive)
        {
            GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.PDeath);
            Debug.Log("player died");
            alive = false;
            gold = 0;
            deathtimer = 5f;
        }
    }

    void Respawn()
    {
        Debug.Log("respawning");
        health = maxhealth;
        mana = maxmana;
        alive = true;
        transform.position = GetSpawnPoint();
    }

    Vector3 GetSpawnPoint()
    {
        if (enemyManager.CurrentArea == 0)
            enemyManager.CurrentArea = 1;
        return playerSpawnList[enemyManager.CurrentArea - 1][Random.Range(0, playerSpawnList[enemyManager.CurrentArea - 1].Count)].transform.position;
    }
}
