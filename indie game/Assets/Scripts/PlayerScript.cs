using UnityEngine;
using System.Collections;

public class PlayerScript : stats {

    int qCost = 15;

	// Use this for initialization
	void Start () {
        maxhealth = 100;
        health = 100;
        maxmana = 100;
        mana = 100;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<AttackScript>().MageAttack(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (mana >= qCost)
            {
                mana -= qCost;
                GetComponent<AttackScript>().MageAttack(1);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mana >= qCost)
            {
                mana -= qCost;
                GetComponent<AttackScript>().MageAttack(2);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (mana >= qCost)
            {
                mana -= qCost;
                GetComponent<AttackScript>().MageAttack(3);
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

    }
}
