using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    

    [SerializeField]
    GameObject HitboxPrefab;
    public Spells spellmanager;

    private float qCooldown;
    private float wCooldown;
    private float eCooldown;
    private float rCooldown;
    
    public void MeleeAttack()
    {
            CheckSpellManager();
            spellmanager.MakeSpellMouse(spellmanager.spellslist[4], gameObject.transform);

        
    }

    public void MageAttackForward(int index, Vector3 forward)
    {

            CheckSpellManager();

            spellmanager.MakeSpellForward(spellmanager.spellslist[index], gameObject.transform, forward);
        
    }

    public void MageAttackMouse(int index)
    {
    
            CheckSpellManager();
         
            spellmanager.MakeSpellMouse(spellmanager.spellslist[index], gameObject.transform);
        
    }
    void CheckSpellManager()
    {
        if (spellmanager == null)
        {
            spellmanager = GameObject.FindObjectOfType<Spells>();
        }
    }
  
    void Update()
    {
       
        if(qCooldown > 0)
            qCooldown -= Time.deltaTime;
        if (wCooldown > 0)
            wCooldown -= Time.deltaTime;
        if (eCooldown > 0)
            eCooldown -= Time.deltaTime;
        if (qCooldown > 0)
            qCooldown -= Time.deltaTime;


    }
}
