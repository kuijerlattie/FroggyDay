using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    

    [SerializeField]
    GameObject HitboxPrefab;
    public Spells spellmanager;
    private float cooldown = 1;
    private float _currentCooldown = 0;
    private float currentCooldown {
        get { return _currentCooldown; }
        set { _currentCooldown = (value > 0)? ((value <= cooldown)? value : cooldown) :  0; } }

    
    public void MeleeAttack()
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;

        }
    }

    public void MageAttack(int index)
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;
            spellmanager.MakeSpell(spellmanager.spellslist[index], gameObject.transform);
        }
    }
  
    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

    }
}
