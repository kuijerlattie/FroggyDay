using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    GameObject HitboxPrefab;
    public Spells spellmanager;

    float[] cooldowns;
    public float[] coolDowns { get { return cooldowns; } }

    AudioSource hudAudio;

    [SerializeField]
    public GameObject spellSoundHelperPrefab;
    
    void Start()
    {
        CheckSpellManager();
        CheckCooldowns();

        hudAudio = GameObject.Find("HUD").GetComponent<AudioSource>();
    }

    /// <summary>
    /// start a melee attack
    /// </summary>
    /// <returns>-1 = programmer error, 0 = succes, 1 = low mana, 2 = cooldown</returns>
    public int MeleeAttack()
    {
            CheckSpellManager();
            spellmanager.MakeSpellMouse(spellmanager.spellslist[4], gameObject.transform);
            PlaySpellSound(spellmanager.spellslist[4].spellSound);
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
            if (spellmanager.spellslist[index].uses != 0)
            {
                if (spellmanager.spellslist[index].learned)
                {
                    if (playerscript == null || playerscript.mana >= spellmanager.spellslist[index].manacost)
                    {
                        if (playerscript != null)
                        {
                            playerscript.mana -= spellmanager.spellslist[index].manacost;
                            playerscript.mana += spellmanager.spellslist[index].selfmana;
                            playerscript.health += spellmanager.spellslist[index].selfheal;
                            if (spellmanager.spellslist[index].uses > 0)
                                spellmanager.spellslist[index].uses -= 1;

                        }
                        cooldowns[index] = spellmanager.spellslist[index].cooldown;
                        spellmanager.MakeSpellForward(spellmanager.spellslist[index], gameObject.transform, forward);
                        PlaySpellSound(spellmanager.spellslist[index].spellSound);
                        return 0;
                    }
                    PlayHudSound(spellmanager.lowOnMana);
                    return 1;
                }
                PlayHudSound(spellmanager.SpellNotLearned);
                return 3;
            }
            PlayHudSound(spellmanager.OutOfCharges);
            return 4;
        }
        PlayHudSound(spellmanager.Cooldown);
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
            if (spellmanager.spellslist[index].uses != 0)
            {
                if (spellmanager.spellslist[index].learned)
                {
                    if (playerscript == null || playerscript.mana >= spellmanager.spellslist[index].manacost)
                    {
                        if (playerscript != null)
                        {
                            playerscript.mana -= spellmanager.spellslist[index].manacost;
                            playerscript.mana += spellmanager.spellslist[index].selfmana;
                            playerscript.health += spellmanager.spellslist[index].selfheal;
                            if (spellmanager.spellslist[index].uses > 0)
                                spellmanager.spellslist[index].uses -= 1;

                        }
                        cooldowns[index] = spellmanager.spellslist[index].cooldown;
                        spellmanager.MakeSpellMouse(spellmanager.spellslist[index], gameObject.transform);
                        PlaySpellSound(spellmanager.spellslist[index].spellSound);
                        return 0;
                    }
                    PlayHudSound(spellmanager.lowOnMana);
                    return 1;
                }
                PlayHudSound(spellmanager.SpellNotLearned);
                return 3;
            }
            PlayHudSound(spellmanager.OutOfCharges);
            return 4;
        }
        PlayHudSound(spellmanager.Cooldown);
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

    void PlayHudSound(AudioClip clip)
    {
        if (hudAudio.clip != clip)
        {
            hudAudio.clip = clip;
            hudAudio.Play();
        }
        else if (!hudAudio.isPlaying)
        {
            hudAudio.Play();
        }
    }

    void PlaySpellSound(AudioClip clip)
    {
        GameObject g = GameObject.Instantiate(spellSoundHelperPrefab);
        g.AddComponent<AudioSource>();
        g.GetComponent<AudioSource>().clip = clip;
        g.AddComponent<SpellSoundHelper>();

    }
}
