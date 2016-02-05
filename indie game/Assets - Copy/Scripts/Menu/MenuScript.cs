using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {


    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject Credits;
    public Slider VolumeSlider;
    public SoundManager soundmanager;

	// Use this for initialization
	void Start () {
        soundmanager = GameObject.FindObjectOfType<SoundManager>();
        soundmanager.MakeSoundObjectLooped(SoundManager.Sounds.MenuMusic);
        ShowMainMenu();
    }

    public void Play()
    {
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        Application.LoadLevel("mninimapscene");
        Settings.SetActive(false);
        Credits.SetActive(false);
    }

    void ShowCredits()
    {
        Credits.SetActive(true);
        Settings.SetActive(false);
        MainMenu.SetActive(false);
    }
    void ShowSettings()
    {
        Settings.SetActive(true);
        Credits.SetActive(false);
        MainMenu.SetActive(false);
    }

    void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        Settings.SetActive(false);
    }

    public void Quit()
    {
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        Application.Quit();
    }
    

    public void SettingsBack()
    {
        //save audio settings
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        ShowMainMenu();
    }

    public void CreditsBack()
    {
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        ShowMainMenu();
    }

    public void GoSettings()
    {
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        ShowSettings();
    }

    public void GoCredits()
    {
        soundmanager.MakeSoundObject(SoundManager.Sounds.Click);
        ShowCredits();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.normalizedValue;
    }

}
