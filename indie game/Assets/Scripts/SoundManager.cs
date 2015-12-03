using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    GameObject SoundObjectPrefab;

    FmodPlayScript musicPlayer;

    Dictionary<Sounds, string> SoundsDatabase = new Dictionary<Sounds, string>();
    void Start () {
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

		SoundsDatabase.Add(Sounds.EMidAttack, "event:/Sounds/EMid/AE2");
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
		SoundsDatabase.Add(Sounds.Music, "event:/Music/Music");

        SoundsDatabase.Add(Sounds.NeedMoreGold, "event:/Sounds/Player/FireB");
        SoundsDatabase.Add(Sounds.OutOfPotions, "event:/Sounds/Player/FireS");

        SoundsDatabase.Add(Sounds.Footstep, "event:/Sounds/Player/Footstep");


        if(GameObject.FindObjectOfType<PlayerScript>() != null)
                musicPlayer = GameObject.FindObjectOfType<SoundManager>().MakeSoundObjectLooped(SoundManager.Sounds.Music);
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
		Music,
        NeedMoreGold,
        OutOfPotions,
        Footstep
    }

    public void MakeSoundObject(Sounds soundeffect)
    {
        GameObject soundObj = GameObject.Instantiate(SoundObjectPrefab);
        FmodPlayScript soundscript = soundObj.GetComponent<FmodPlayScript>();
        soundscript.PlaySound(SoundsDatabase[soundeffect]);
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
}


