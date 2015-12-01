using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    string currentCoroutine = "";

    bool hasStarted = false;
    bool walkedInstructions = false;
    bool hasWalked = false;
    bool rotateInstructions = false;
    bool hasRotatedCamera = false;
    bool enemyInstructions = false;
    bool hasKilledEnemy = false;
    bool potionInstruction = false;
    bool hasUsedPotion = false;
    bool destroyme = false;
    public float WaitTimeInSeconds = 5;

    float playerrotationstart;

    PlayerScript player;

    GameObject enemy;

    public GameObject WalkTarget;
    public GameObject meleeEnemyPrefab;
    public GameObject TutorialEnemySpawn;
    public GameObject HealthpotionSpawn;
    public GameObject IntroActor;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            SingSong();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StopCoroutine(currentCoroutine);
            hasStarted = true;
            walkedInstructions = true;
            hasWalked = true;
            rotateInstructions = true;
            hasRotatedCamera = true;
            enemyInstructions = true;
            hasKilledEnemy = true;
            potionInstruction = true;
            player.health = player.maxhealth;
        }

        if (destroyme)
        {
            GameObject.Destroy(gameObject);
        }

        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = "StartTutorial";
            StartCoroutine("StartTutorial");
        }
        else if (!hasWalked)
        {
            if (walkedInstructions && CheckWalked())
            {
                hasWalked = true;
                currentCoroutine = "StartRotation";
                StartCoroutine("StartRotation");
            }
        }
        else if (!hasRotatedCamera)
        {
            if (rotateInstructions && CheckRotation())
            {
                hasRotatedCamera = true;
                currentCoroutine = "StartEnemy";
                StartCoroutine("StartEnemy");
            }
        }
        else if (!hasKilledEnemy)
        {
            if (enemyInstructions && CheckEnemy())
            {
                hasKilledEnemy = true;
                currentCoroutine = "StartPotion";
                StartCoroutine("StartPotion");
            }
        }
        else if (!hasUsedPotion)
        {
            if (potionInstruction && CheckPotion())
            {
                hasUsedPotion = true;
                StartCoroutine(EndTutorial());
            }
        }
	}

    IEnumerator StartTutorial()
    {
        Debug.Log("StartedTutorial");
        ShowText("Press Y to skip this tutorial at any time!");
        yield return new WaitForSeconds(5f);
        ShowText("Hello Jian, we will prepare your journey today, to become the master! \r\n Walk to the target that just appeared Jian!");
        yield return new WaitForSeconds(WaitTimeInSeconds);
        WalkTarget.GetComponent<MeshRenderer>().enabled = true;//turn on walk target
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
        ShowText("Now rotate the camera around yourself Jian. you can do this by pressing A or D");
        //say rotation text
        yield return new WaitForSeconds(WaitTimeInSeconds);//set rotation target
        rotateInstructions = true;
        yield return new WaitForSeconds(0.01f);
    }

    bool CheckRotation()
    {
        if (playerrotationstart < FindObjectOfType<CameraController>().rotation || playerrotationstart > FindObjectOfType<CameraController>().rotation)
            return true;
        return false;
    }

    IEnumerator StartEnemy()
    {
        ShowText("Look at the Spellbar at the bottom of your screen Jian. Your first spell will pop up in there now");
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
        ShowText("An enemy appeared Jian! Kill him by aiming at him with your mouse and then pressing Q to cast a spell");

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
        ShowText("If you lose health when fighting enemies, you can use a health potion to heal yourself. \r\n Pick up the health potion that i just spawned for you, then press 1 to drink it.");
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

    IEnumerator EndTutorial()
    {
        ShowText("I will be in the yard if you need me for further guidance.");
        player.health = player.maxhealth;
        yield return new WaitForSeconds(10);
        FindObjectOfType<EnemyManager>().StartWave(1);
        GameObject.Destroy(IntroActor);
        destroyme = true;
        yield return new WaitForSeconds(0.01f);

    }

    void ShowText(string ptext)
    {
        ResizeTextbox[] textboxes = GameObject.FindObjectsOfType<ResizeTextbox>();
        ResizeTextbox thistextbox = null;
        foreach(ResizeTextbox textbox in textboxes)
        {
            if(textbox._text == text)
            {
                thistextbox = textbox;
                break;
            }
        }
        if(thistextbox != null)
        {
            thistextbox.UpdateSize(ptext);
        }
        text.text = ptext; //not confusing at all, text text text      -         text.text.fbx.txt.meta.text.confuse
    }

    void SingSong()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}
