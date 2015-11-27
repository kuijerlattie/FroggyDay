using UnityEngine;
using System.Collections;

public abstract class EnemyBase : stats {

    protected EnemyManager manager;

    protected NavMeshAgent agent;

    //protected float movespeed;
    protected int difficulty;
    protected GameObject target;
    protected float attackrange;

    public bool isWaveEnemy = false;

    protected float stunnedSeconds = 0;

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
        if (stunnedSeconds > 0)
        {
            stunnedSeconds -= Time.deltaTime;
            return;
        }
        agent.SetDestination(target.transform.position);
	}

    
    public override void Hit(int dmg)
    {
        health -= dmg;
        Debug.Log(gameObject.name + "'s health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isWaveEnemy)
            manager.RemoveEnemy();
        Healingpotion hppot = new Healingpotion();
        hppot.healingValue = 10;
        hppot.Drop(transform.position);
        Manapotion mppot = new Manapotion();
        mppot.manaValue = 10;
        mppot.Drop(transform.position);
        GameObject.FindObjectOfType<PlayerScript>().LootGold(gold);
        GameObject.Destroy(gameObject);
    }
}
