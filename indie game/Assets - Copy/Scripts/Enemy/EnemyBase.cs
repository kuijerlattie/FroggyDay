﻿using UnityEngine;
using System.Collections;

public abstract class EnemyBase : stats {

    protected EnemyManager manager;

    protected NavMeshAgent agent;

    //protected float movespeed;
    public float viewdistance = 10f;

    protected int difficulty;
    protected GameObject target;
    protected Vector3 targetpos;
    protected float attackrange;

    public bool isWaveEnemy = false;
    public int area;

    [HideInInspector]
    public bool canDrop = true;
    public Dropables AlwaysDrops = Dropables.Nothing;

    public float dropChance = 50;   //standard 50%
   

    protected float stunnedSeconds = 0;


    public enum Dropables
    {
        Nothing,
        HealthPotion,
        ManaPotion
    }

    // Use this for initialization
    protected void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        maxhealth = 500;
        health = 100;
        gold = Random.Range(50, 101);
       
        manager = GameObject.FindObjectOfType<EnemyManager>();

    }

    public void Stun(float seconds)
    {
        stunnedSeconds += seconds;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        else
        {
            if (stunnedSeconds > 0)
            {
                stunnedSeconds -= Time.deltaTime;
                return;
            }
            if (Vector2.Distance(this.transform.position, target.transform.position) <= viewdistance) //if the player is too far away, dont chase him anymore
                agent.SetDestination(target.transform.position);
            else
                agent.SetDestination(this.transform.position);
        }
	}

    
    public override void Hit(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    protected Item GetRandomPickup()
    {
        Item item = null;   //returning null means no item 
        dropChance = dropChance > 0 ? (dropChance < 100 ? dropChance : 100) : 0;    //keep between 0 and 100
        int rnd = Random.Range(0, (int)(2*(100/ dropChance)));
        switch (rnd)
        {
            case 0:
               item = new Healingpotion() { healingValue = 10 };
               break;
            case 1:
                item = new Manapotion() { manaValue = 10 };
                break;
        }
        return item;
    }

    protected Item DropAlwaysDrop()
    {
        Item item = null;
        switch (AlwaysDrops)
        {
            case Dropables.Nothing: //item stays null
                break;
            case Dropables.HealthPotion:
                item = new Healingpotion() { healingValue = 10 };
                break;
            case Dropables.ManaPotion:
                item = new Manapotion() { manaValue = 10 };
                break;
        }
        if(item != null)
        {
            item.Drop(transform.position);
        }
        return item;
    }

    protected Item DropRandomPickup(Vector3 position)
    {
        Item item = GetRandomPickup();
        if (item != null)
            item.Drop(position);
        return item;
    }

    public override void Die()
    {
        if(manager == null) manager = GameObject.FindObjectOfType<EnemyManager>();
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.EMidDeath);
        manager.RemoveEnemy();

        if (canDrop)
        {
            DropRandomPickup(transform.position);
            DropAlwaysDrop();
        }


        GameObject.FindObjectOfType<PlayerScript>().LootGold(gold);
        GameObject.Destroy(gameObject);
    }
}