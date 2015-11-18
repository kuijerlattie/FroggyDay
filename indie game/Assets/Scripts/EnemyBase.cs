using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

    protected float movespeed;
    protected float hitpoints;
    protected int difficulty;
    protected GameObject target;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Hit(float dmg)
    {
        hitpoints -= dmg;
        if (hitpoints <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
