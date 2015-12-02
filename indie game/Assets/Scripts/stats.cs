using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour {

    private int _health;
    public int health { get { return _health; } set { _health = value > 0 ? (value <= maxhealth? value : maxhealth) : 0; } }
    [HideInInspector]
    public int maxhealth;

    private float _mana;
    public float mana { get { return _mana; } set { _mana = value > 0 ? (value <= maxmana ? value : maxmana) : 0; } }
    [HideInInspector]
    public float maxmana;

    private int _gold;
    [HideInInspector]
    public int gold { get { return _gold; } set { _gold = value; } }


    public virtual void Hit(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplySlow(float percentage, float seconds)
    {
        float _movespeed = movespeed;
        movespeed = (movespeed/100) * (100-percentage);
        GetComponent<NavMeshAgent>().speed = movespeed;
        StartCoroutine(RemoveSlow(_movespeed - movespeed, seconds));
    }

    private IEnumerator RemoveSlow(float slow, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movespeed += slow;
        GetComponent<NavMeshAgent>().speed = movespeed;
    }

    public void HitOverTime(int dmg, int seconds)
    {
        StartCoroutine(HitPerSecond(dmg, seconds));
    }

    private IEnumerator HitPerSecond(int dmg, int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Hit(dmg);
            yield return new WaitForSeconds(1);
        }
    }

    /*
    int attack { get; set; }
    int defense { get; set; }
    */
    public float movespeed { get { return GetComponent<NavMeshAgent>().speed; }
                             set { GetComponent<NavMeshAgent>().speed = value; } }

    public abstract void Die();
}
