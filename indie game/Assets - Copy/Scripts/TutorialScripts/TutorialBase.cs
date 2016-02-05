using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialBase : MonoBehaviour {

    public string nextLevel;

    Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected void DisableWalking()
    {
        GameObject.FindObjectOfType<playerMovementSmart>().lockMovement = true;
    }

    protected void EnableWalking()
    {
        GameObject.FindObjectOfType<playerMovementSmart>().lockMovement = false;

    }

    protected void DisableRotation()
    {
        GameObject.FindObjectOfType<CameraController>().lockRotation = true;
    }

    protected void EnableRotation()
    {
        GameObject.FindObjectOfType<CameraController>().lockRotation = false;
    }

    protected void CheckSkip()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //skip tutorial
            Application.LoadLevel(nextLevel);
        }
    }

    protected void NextLevel()
    {
        Application.LoadLevel(nextLevel);
    }

    protected void ShowText(string ptext)
    {
        if (text == null)
        {
            text = GameObject.Find("TutorialTextBox").GetComponent<Text>();
        }
        ResizeTextbox[] textboxes = GameObject.FindObjectsOfType<ResizeTextbox>();
        ResizeTextbox thistextbox = null;
        foreach (ResizeTextbox textbox in textboxes)
        {
            if (textbox._text == text)
            {
                thistextbox = textbox;
                break;
            }
        }
        if (thistextbox != null)
        {
            thistextbox.UpdateSize(ptext);
        }
        text.text = ptext; //not confusing at all, text text text      -         text.text.fbx.txt.meta.text.confuse
    }
}
