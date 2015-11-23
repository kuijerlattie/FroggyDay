using UnityEngine;
using System.Collections;

public abstract class EnemyBase : stats {

    protected EnemyManager manager;

    protected NavMeshAgent agent;

    //protected float movespeed;
    protected int difficulty;
    protected GameObject target;
    protected float attackrange;

	// Use this for initialization
	protected void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        maxhealth = 10;
        health = 10;
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
    protected void Update()
    {
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
        manager.RemoveEnemy();
        GameObject.Destroy(gameObject);
    }
}
