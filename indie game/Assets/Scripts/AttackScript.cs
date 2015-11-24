using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    GameObject HitboxPrefab;
    public Spells spellmanager;

    float[] cooldowns;
    
    
    void Start()
    {
        CheckSpellManager();
        CheckCooldowns();
    }

    public void MeleeAttack()
    {
            CheckSpellManager();
            spellmanager.MakeSpellMouse(spellmanager.spellslist[4], gameObject.transform);
    }
    
    public void MageAttackForward(int index, Vector3 forward)
    {
        if (!CheckCooldowns())
            return;
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
            }
        }
        
    }

    public void MageAttackMouse(int index)
    {
        if (!CheckCooldowns())
            return;
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
            }
        }

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
