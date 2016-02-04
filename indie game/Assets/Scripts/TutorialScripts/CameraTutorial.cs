using UnityEngine;
using System.Collections;

public class CameraTutorial : TutorialBase {

    bool started = false;
    bool rotateInstructions = false;
    bool done = false;
    public float WaitTimeInSeconds = 5f;

    int rotationA = 0;
    int rotationD = 0;
    int rotationTreshold = 10;

	// Use this for initialization
	void Start () {

        DisableRotation();
        DisableWalking();

	}
	
	// Update is called once per frame
	void Update () {

        if (!started)
        {
            StartCoroutine("StartRotation");
            started = true;
        }
        CheckSkip();
        if (CheckRotation())
        {
            NextLevel();
        }
	}

    IEnumerator StartRotation()
    {

        //TODO fix rotation shit
        //playerrotationstart = FindObjectOfType<CameraController>().rotation;
        ShowText("Hello Jian, I will help you prepare your exam today, to become the master!");
        //play monologue
        yield return new WaitForSeconds(WaitTimeInSeconds);
        ShowText("You must get ready for the dangerous journey. Do you see the ancient scroll? Press A and D to rotate the camera");
        EnableRotation();
        //show buttons onscreen?
        //play monologue
        //say rotation text
        yield return new WaitForSeconds(WaitTimeInSeconds);//set rotation target
        rotateInstructions = true;
        yield return new WaitForSeconds(0.01f);
    }

    bool CheckRotation()
    {
        if (rotateInstructions && !done)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotationA++;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotationD++;
            }

            Debug.Log("total A: " + rotationA + " total D: " + rotationD);

            if (rotationD >= rotationTreshold && rotationA >= rotationTreshold)
            {
                return true;
            }
        }
        return false;
    }


}
