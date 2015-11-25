using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    GameObject HitboxPrefab;
    public Spells spellmanager;

    float[] cooldowns;
    public float[] coolDowns { get { return cooldowns; } }
    
    
    void Start()
    {
        CheckSpellManager();
        CheckCooldowns();
    }

    /// <summary>
    /// start a melee attack
    /// </summary>
    /// <returns>-1 = programmer error, 0 = succes, 1 = low mana, 2 = cooldown</returns>
    public int MeleeAttack()
    {
            CheckSpellManager();
            spellmanager.MakeSpellMouse(spellmanager.spellslist[4], gameObject.transform);
            return 0;
    }

    /// <summary>
    /// cast a spell from caster towards target position
    /// </summary>
    /// <param name="index"></param>
    /// <param name="forward"></param>
    /// <returns>-1 = programmer error, 0 = succes, 1 = low mana, 2 = cooldown</returns>
    public int MageAttackForward(int index, Vector3 forward)
    {
        if (!CheckCooldowns())
            return -1;
        CheckSpellManager();

        PlayerScript playerscript = GetComponent<PlayerScript>();
    
        if (cooldowns[index] <= 0)
        {
            if (playerscript == null || playerscript.mana >= spellmanager.spellslist[index].manacost)
            {
                if(playerscript != null)
                {
                    playerscript.mana -= spellmanager.spellslist[index].manacost;
                }
                cooldowns[index] = spellmanager.spellslist[index].cooldown;
                spellmanager.MakeSpellForward(spellmanager.spellslist[index], gameObject.transform, forward);
                return 0;
            }
            return 1;
        }
        return 2;
        
    }
    /// <summary>
    /// cast a spell at target position
    /// </summary>
    /// <param name="index"></param>
    /// <returns>-1 = programmer error, 0 = succes, 1 = low mana, 2 = cooldown</returns>
    public int MageAttackMouse(int index)
    {
        if (!CheckCooldowns())
            return -1;
        CheckSpellManager();

        PlayerScript playerscript = GetComponent<PlayerScript>();

        if (cooldowns[index] <= 0)
        {
            if (playerscript == null || playerscript.mana >= spellmanager.spellslist[index].manacost)
            {
                if (playerscript != null)
                {
                    playerscript.mana -= spellmanager.spellslist[index].manacost;
                }
                cooldowns[index] = spellmanager.spellslist[index].cooldown;
                spellmanager.MakeSpellMouse(spellmanager.spellslist[index], gameObject.transform);
                return 0;
            }
            return 1;
        }
        return 2;

    }

    bool CheckSpellManager()
    {
        if (spellmanager == null)
        {
            spellmanager = GameObject.FindObjectOfType<Spells>();
        }
        return true;
    }

    bool CheckCooldowns()
    {
        if(!CheckSpellManager())
            return false;
        if (cooldowns == null)
        {
            cooldowns = new float[spellmanager.spellslist.Count];
            for (int i = 0; i < cooldowns.Length; i++)
            {
                cooldowns[i] = 0.0f;
            }
        }
        return true;
    }

    void Update()
    {
        if (CheckCooldowns())
        {
            for (int i = 0; i < cooldowns.Length; i++)
            {
                if (cooldowns[i] > 0)
                    cooldowns[i] -= Time.deltaTime;
            }
        }


    }
}
