using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WalkingTutorial : TutorialBase {

    bool started = false;
    bool walkedInstructions = false;
    bool hasWalked = false;

    public float WaitTimeInSeconds = 5;

    PlayerScript player;

    public GameObject WalkTarget;

	// Use this for initialization
	void Start () {
        DisableWalking();

	    player = FindObjectOfType<PlayerScript>();
        if (WalkTarget == null)
            WalkTarget = GameObject.Find("TutorialWalkTarget");

        
	}

    IEnumerator StartTutorial()
    {
        ShowText("The ancient scroll is to master the air strike. It  should be around! Please, bring it to me");
        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(WaitTimeInSeconds);
        WalkTarget.GetComponent<MeshRenderer>().enabled = true;//turn on walk target
        walkedInstructions = true;
        yield return new WaitForSeconds(5f);
        NextLevel();

    }
	
	// Update is called once per frame
	void Update () {
        if (!started)
        {
            StartCoroutine("StartTutorial");
            started = true;
        }
        if (walkedInstructions && CheckWalked())
        {
            NextLevel();
        }
        CheckSkip();
	}

    bool CheckWalked()
    {
        if (Vector3.Distance(player.transform.position, WalkTarget.transform.position) <= 0.5)
        {
            WalkTarget.GetComponent<MeshRenderer>().enabled = false;
            return true;
        }
        return false;
    }

    
}
