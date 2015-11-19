﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackScript : MonoBehaviour {

    

    [SerializeField]
    GameObject HitboxPrefab;
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
            AttackCollider.MakeAttack(HitboxPrefab, gameObject.transform.position + gameObject.transform.forward *2 + gameObject.transform.up, 3, gameObject.transform);
        }
    }

    public void MageAttack()
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;
            AttackCollider.MakeSpell(HitboxPrefab, gameObject.transform.position + gameObject.transform.forward *2 + gameObject.transform.up, 3, gameObject.transform);
        }
    }
    public void BasicSpell()
    {
        if (currentCooldown == 0)
        {
            currentCooldown = cooldown;
            AttackCollider.MakeSpellBasic(HitboxPrefab, gameObject.transform.position + gameObject.transform.up, 3, gameObject.transform);
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
