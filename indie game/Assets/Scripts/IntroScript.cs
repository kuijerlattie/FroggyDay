﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    bool hasStarted = false;
    bool walkedInstructions = false;
    bool hasWalked = false;
    bool rotateInstructions = false;
    bool hasRotatedCamera = false;
    bool enemyInstructions = false;
    bool hasKilledEnemy = false;
    bool potionInstruction = false;
    bool hasUsedPotion = false;
    public float WaitTimeInSeconds = 5;

    float playerrotationstart;

    PlayerScript player;

    GameObject enemy;

    public GameObject WalkTarget;
    public GameObject meleeEnemyPrefab;
    public GameObject TutorialEnemySpawn;
    public GameObject HealthpotionSpawn;

    Text text;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerScript>();
        text = GameObject.Find("TutorialTextBox").GetComponent<Text>();
        if (WalkTarget == null)
            WalkTarget = GameObject.Find("TutorialWalkTarget");
	}
	
	// Update is called once per frame
	void Update () {
        //check tutorial break here
        Debug.Log("updateing intro shizzle");
        if (Input.GetKeyDown(KeyCode.F))
        {
            SingSong();
        }

        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(StartTutorial());
            Debug.Log("returned started tutorial");
        }
        else if (!hasWalked)
        {
            if (walkedInstructions && CheckWalked())
            {
                hasWalked = true;
                StartCoroutine(StartRotation());
            }
        }
        else if (!hasRotatedCamera)
        {
            if (rotateInstructions && CheckRotation())
            {
                hasRotatedCamera = true;
                StartCoroutine(StartEnemy());
            }
        }
        else if (!hasKilledEnemy)
        {
            if (enemyInstructions && CheckEnemy())
            {
                hasKilledEnemy = true;
                StartCoroutine(StartPotion());
            }
        }
        else if (!hasUsedPotion)
        {
            if (potionInstruction && CheckPotion())
            {
                hasUsedPotion = true;
                EndTutorial();
            }
        }
	}

    IEnumerator StartTutorial()
    {
        Debug.Log("StartedTutorial");
        ShowText("I no speak london, cyka blyet");
        yield return new WaitForSeconds(WaitTimeInSeconds);
        ShowText("Hello teddybear, Walk to the target!");
        yield return new WaitForSeconds(WaitTimeInSeconds);
        WalkTarget.GetComponent<MeshRenderer>().enabled = true;//turn on walk target
        Debug.Log("finished start tutorial");
        walkedInstructions = true;
        yield return new WaitForSeconds(0.01f);
        
    }

    bool CheckWalked()
    {
        Debug.Log("Distance to target: " + Vector3.Distance(player.transform.position, WalkTarget.transform.position));
        if (Vector3.Distance(player.transform.position, WalkTarget.transform.position) <= 3)
        {
            WalkTarget.GetComponent<MeshRenderer>().enabled = false;
            return true;
        }
        return false;
    }

    IEnumerator StartRotation()
    {
        playerrotationstart = FindObjectOfType<CameraController>().rotation;
        ShowText("Great, you know walking. Now spinbot!");
        //say rotation text
        yield return new WaitForSeconds(WaitTimeInSeconds);
        ShowText("Did you know you can also press the f button to make me sing American rap song?");
        //set rotation target
        rotateInstructions = true;
        yield return new WaitForSeconds(0.01f);
    }

    bool CheckRotation()
    {
        Debug.Log("player start rotation: " + playerrotationstart + "player current rotation: " + FindObjectOfType<CameraController>().rotation);
        if (playerrotationstart < FindObjectOfType<CameraController>().rotation || playerrotationstart > FindObjectOfType<CameraController>().rotation)
            return true;
        return false;
    }

    IEnumerator StartEnemy()
    {
        ShowText("You are real spinbot master. Now it is time to learn how to attack.");
        //say enemy text
        yield return new WaitForSeconds(WaitTimeInSeconds);
        ShowText("I shall summon you a teddybear. Press q to hit it with your spell. dont forget to aim at the teddybear by putting your mouse cursor on him!");
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
        ShowText("I stunned the teddybear so he cannot harm you. Press q to hit it with your spell. dont forget to aim at the teddybear by putting your mouse cursor on him!");

        yield return new WaitForSeconds(0.01f);
    }

    bool CheckEnemy()
    {
        if (enemy == null)
        {
            return true;
        }
        return false;
    }

    IEnumerator StartPotion()
    {
        player.Hit(10); //hit player to make sure he is not at full health anymore
        //say potion text
        ShowText("If you lose health when fighting enemies, you can use a health potion to heal yourself. Pick up the health potion that i just spawned for you, then press 1 to drink it.");
        Healingpotion hppot = new Healingpotion();
        hppot.healingValue = 100;
        hppot.Drop(HealthpotionSpawn.transform.position);
        potionInstruction = true;
        yield return new WaitForSeconds(0.01f);
    }

    bool CheckPotion()
    {
        Debug.Log("Checking player health");
        if (player.health >= 100) //cheap way to check if he used the healthpot. remember that we damage him before spawning a healthpot to make sure he isnt on 100% health anymore when we check this
        {
            return true;
        }
        return false;
    }

    void EndTutorial()
    {
        ShowText("Thats it, tutorial is over. plz go away now!");
    }

    void ShowText(string ptext)
    {
        text.text = ptext; //not confusing at all, text text text
    }

    void SingSong()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}