using UnityEngine;
using System.Collections;

public abstract class EnemyBase : stats {

    protected EnemyManager manager;

    protected NavMeshAgent agent;

    //protected float movespeed;
    public float viewdistance = 10f;

    protected int difficulty;
    protected GameObject target;
    protected float attackrange;

    public bool isWaveEnemy = false;
    public int area;

    protected float stunnedSeconds = 0;

    // Use this for initialization
    protected void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        maxhealth = 500;
        health = 100;
        gold = Random.Range(50, 101);
       
        manager = GameObject.FindObjectOfType<EnemyManager>();

        if (!isWaveEnemy)
        {
            //not part of a wave, link to area
            manager.AddToArea(area);
        }
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
        if (Vector2.Distance(this.transform.position, target.transform.position) <= viewdistance)
            agent.SetDestination(target.transform.position);
        else
            agent.SetDestination(this.transform.position);
	}

    
    public override void Hit(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isWaveEnemy)
            manager.RemoveEnemy();
        else
            manager.RemoveFromArea(area);
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
