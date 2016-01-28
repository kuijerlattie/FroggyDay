using UnityEngine;
using System.Collections;

public class AttackTutorial : TutorialBase {

    bool started = false;
    public GameObject meleeEnemyPrefab;
    GameObject enemy;
    PlayerScript player;
    bool enemyInstructions = false;
    public GameObject TutorialEnemySpawn;
    public GameObject HealthpotionSpawn;
    bool killedEnemy = false;
    bool potionInstruction = false;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!started)
        {
            StartCoroutine("StartEnemy");
            started = true;
        }
        CheckSkip();
        if (CheckEnemy() && CheckPotion() && potionInstruction)
        {
            NextLevel();
        }
        
        CheckSkip();
	}

    IEnumerator StartEnemy()
    {
        ShowText("An enemy Jian! Kill him!");
        //add voiceover
        player.SetSpell(PlayerScript.SpellSlots.spellQ, 1);
        //set enemy target;
        yield return new WaitForSeconds(2f);
        enemy = (GameObject)GameObject.Instantiate(meleeEnemyPrefab);
        enemy.transform.position = TutorialEnemySpawn.transform.position;
        enemy.GetComponent<EnemyBase>().isWaveEnemy = false;
        enemy.GetComponent<EnemyBase>().area = 1;
        enemy.GetComponent<EnemyBase>().health = 1;
        enemy.GetComponent<EnemyBase>().canDrop = false;
        yield return new WaitForSeconds(2f);
        enemy.GetComponent<EnemyBase>().Stun(600);
        enemyInstructions = true;

        yield return new WaitForSeconds(0.01f);
    }

    bool CheckEnemy()
    {
        if (enemyInstructions && enemy == null)
        {
            StartCoroutine("StartPotion");
            return true;
        }
        return false;
    }

    IEnumerator StartPotion()
    {
        player.Hit(10); //hit player to make sure he is not at full health anymore
        //say potion text
        ShowText("Drops go to your inventory and can be used or sold. /");
        //add voiceover
        Healingpotion hppot = new Healingpotion();
        hppot.healingValue = 100;
        hppot.Drop(HealthpotionSpawn.transform.position);
        potionInstruction = true;
        yield return new WaitForSeconds(0.01f);
    }

    bool CheckPotion()
    {
        if (player.health >= 100) //cheap way to check if he used the healthpot. remember that we damage him before spawning a healthpot to make sure he isnt on 100% health anymore when we check this
        {
            return true;
        }
        return false;
    }

}
