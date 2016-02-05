using UnityEngine;
using System.Collections;

public class GameStartScript : MonoBehaviour {

    bool started = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!started)
        {
            FindObjectOfType<PlayerScript>().SetSpell(PlayerScript.SpellSlots.spellQ, 1);
            FindObjectOfType<EnemyManager>().StartWave(1);
            started = true;
        }

	}
}
