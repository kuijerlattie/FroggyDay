using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    GameObject SoundObjectPrefab;

    FmodPlayScript musicPlayer;
    bool playerhealthdown = false;

    Dictionary<Sounds, string> SoundsDatabase = new Dictionary<Sounds, string>();
    void Start () {
        SetAllSounds();

        //if the player is in the scene (so not the menu) the ingame music is played instead
        if(GameObject.FindObjectOfType<PlayerScript>() != null)
                musicPlayer = MakeSoundObjectLooped(SoundManager.Sounds.Music1);
    }

    public void MakeSoundObject(Sounds soundeffect, int seconds = 4)
    {
        GameObject soundObj = GameObject.Instantiate(SoundObjectPrefab);
        FmodPlayScript soundscript = soundObj.GetComponent<FmodPlayScript>();
        soundscript.PlaySound(SoundsDatabase[soundeffect], seconds);
    }

    void Update()
    {
        if (GameObject.FindObjectOfType<PlayerScript>().health <= 99 && !playerhealthdown)
        {
            musicPlayer.StopSoundLooped();
            musicPlayer = MakeSoundObjectLooped(SoundManager.Sounds.Music2);
            playerhealthdown = true;
        }

        if (GameObject.FindObjectOfType<PlayerScript>().health > 99 && playerhealthdown)
        {
            musicPlayer.StopSoundLooped();
            musicPlayer = MakeSoundObjectLooped(SoundManager.Sounds.Music1);
            playerhealthdown = false;
        }
    }

    public void StopLooping(FmodPlayScript fmodplayscript)
    {
        if (fmodplayscript == null)
            return;
        fmodplayscript.StopSoundLooped();
    }

    public FmodPlayScript MakeSoundObjectLooped(Sounds soundeffect)
    {
        GameObject soundObj = GameObject.Instantiate(SoundObjectPrefab);
        FmodPlayScript soundscript = soundObj.GetComponent<FmodPlayScript>();
        soundscript.PlaySoundLooped(SoundsDatabase[soundeffect]);
        return soundscript;
    }

    void SetAllSounds()
    {
        SoundsDatabase.Add(Sounds.FireB, "event:/Sounds/Player/FireB");
        SoundsDatabase.Add(Sounds.FireS, "event:/Sounds/Player/FireS");
        SoundsDatabase.Add(Sounds.IceB, "event:/Sounds/Player/IceB");
        SoundsDatabase.Add(Sounds.IceS, "event:/Sounds/Player/IceS");
        SoundsDatabase.Add(Sounds.WindB, "event:/Sounds/Player/WindB");
        SoundsDatabase.Add(Sounds.WindS, "event:/Sounds/Player/WindS");
        SoundsDatabase.Add(Sounds.Spell1, "event:/Sounds/Player/Spell1");

        SoundsDatabase.Add(Sounds.PDamage, "event:/Sounds/Player/Damage");
        SoundsDatabase.Add(Sounds.PDeath, "event:/Sounds/Player/Death");
        SoundsDatabase.Add(Sounds.Heal, "event:/Sounds/Player/Heal");
        SoundsDatabase.Add(Sounds.Mana, "event:/Sounds/Player/Mana");
        SoundsDatabase.Add(Sounds.LowHP, "event:/Sounds/Player/Low on Health");
        SoundsDatabase.Add(Sounds.LowMana, "event:/Sounds/Player/Low on mana");
        SoundsDatabase.Add(Sounds.AbillityBuff, "event:/Sounds/Player/Abiltiy_Buff");
        SoundsDatabase.Add(Sounds.Cooldown, "event:/Sounds/Player/Spell on CD");

        SoundsDatabase.Add(Sounds.ESmallAttack, "event:/Sounds/ESmall/EA1");
        SoundsDatabase.Add(Sounds.ESmallDeath, "event:/Sounds/ESmall/EDeath");
        SoundsDatabase.Add(Sounds.ESsmallMove, "event:/Sounds/ESmall/Move");

        SoundsDatabase.Add(Sounds.EMidAttack, "event:/Sounds/EMid/EA2");
        SoundsDatabase.Add(Sounds.EMidDeath, "event:/Sounds/EMid/EDeath");
        SoundsDatabase.Add(Sounds.EMidMove, "event:/Sounds/EMid/Move");

        SoundsDatabase.Add(Sounds.EBigAttack, "event:/Sounds/EBig/AE3");
        SoundsDatabase.Add(Sounds.EBigDeath, "event:/Sounds/EBig/EDeath");
        SoundsDatabase.Add(Sounds.EBigMove, "event:/Sounds/EBig/Move");

        SoundsDatabase.Add(Sounds.PickUpItem, "event:/Sounds/Enviroment/PickUpItem");
        SoundsDatabase.Add(Sounds.PickUpCoin, "event:/Sounds/Enviroment/PickUpCoin");
        SoundsDatabase.Add(Sounds.OpenChest, "event:/Sounds/Enviroment/OpenChest");
        SoundsDatabase.Add(Sounds.BuyItem, "event:/Sounds/Enviroment/Buy an Item");

        SoundsDatabase.Add(Sounds.MenuMusic, "event:/Menu/MenuMusic");
        SoundsDatabase.Add(Sounds.Click, "event:/Menu/Click");  
        SoundsDatabase.Add(Sounds.Music1, "event:/Music/Music1");
        SoundsDatabase.Add(Sounds.Music2, "event:/Music/Music2");

        SoundsDatabase.Add(Sounds.NeedMoreGold, "event:/Sounds/Player/FireB");
        SoundsDatabase.Add(Sounds.OutOfPotions, "event:/Sounds/Player/FireS");

        SoundsDatabase.Add(Sounds.Footstep, "event:/Sounds/Player/Footstep");

        SoundsDatabase.Add(Sounds.VO1, "event:/VoiceOver/Tutorial/VO1");
        SoundsDatabase.Add(Sounds.VO2, "event:/VoiceOver/Tutorial/VO2");
        SoundsDatabase.Add(Sounds.VO3, "event:/VoiceOver/Tutorial/VO3");
        SoundsDatabase.Add(Sounds.VO4, "event:/VoiceOver/Tutorial/VO4");
        SoundsDatabase.Add(Sounds.VO5, "event:/VoiceOver/Tutorial/VO5");
        SoundsDatabase.Add(Sounds.VO6, "event:/VoiceOver/Tutorial/VO6");

        SoundsDatabase.Add(Sounds.SenseiProud, "event:/VoiceOver/VoiceEffects/SenseiProud");
        SoundsDatabase.Add(Sounds.OhNo, "event:/VoiceOver/VoiceEffects/OhNo");
        SoundsDatabase.Add(Sounds.WatchHP, "event:/VoiceOver/VoiceEffects/WatchHP");
        SoundsDatabase.Add(Sounds.GoodJob, "event:/VoiceOver/VoiceEffects/GoodJob");
    }


    public enum Sounds
    {
        FireB,
        FireS,
        IceB,
        IceS,
        WindB,
        WindS,
        Spell1,
        PDamage,
        PDeath,
        Heal,
        Mana,
        LowHP,
        LowMana,
        AbillityBuff,
        Cooldown,
        ESmallAttack,
        ESmallDeath,
        ESsmallMove,
        EMidAttack,
        EMidDeath,
        EMidMove,
        EBigAttack,
        EBigDeath,
        EBigMove,
        PickUpItem,
        PickUpCoin,
        OpenChest,
        BuyItem,
        MenuMusic,
        Click,
        Music1,
        Music2,
        NeedMoreGold,
        OutOfPotions,   //
        Footstep,   //

        VO1,    //
        VO2,    //
        VO3,    //
        VO4,    //
        VO5,    //
        VO6,    //

        SenseiProud,
        OhNo,   //
        WatchHP,    //
        GoodJob //


    }


}


