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
            if (mana >= qCost)
            {
                mana -= qCost;
                GetComponent<AttackScript>().MageAttack();
                Debug.Log("mana left");
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }
	}
}
