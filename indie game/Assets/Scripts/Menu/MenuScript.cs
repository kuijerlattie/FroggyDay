using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.MenuMusic);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.Click);
        Application.LoadLevel("mninimapscene");
    }

    public void Quit()
    {
        GameObject.FindObjectOfType<SoundManager>().MakeSoundObject(SoundManager.Sounds.Click);
        Application.Quit();
    }
}
