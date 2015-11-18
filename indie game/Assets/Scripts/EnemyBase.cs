﻿using UnityEngine;
using System.Collections;

public class EnemyBase : stats {

    protected NavMeshAgent agent;

    protected float movespeed;
    protected int difficulty;
    protected GameObject target;
    protected float attackrange;

	// Use this for initialization
	protected void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
    protected void Update()
    {
        agent.SetDestination(target.transform.position);
	}

    public void Hit(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
