using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    GameObject SoundObjectPrefab;

    Dictionary<Sounds, string> SoundsDatabase = new Dictionary<Sounds, string>();
    void Start () {
        SoundsDatabase.Add(Sounds.FireB, "event:/Sounds/Player/FireB");
        SoundsDatabase.Add(Sounds.FireS, "event:/Sounds/Player/FireS");
    }

    
    public enum Sounds
    {
        FireB,
        FireS
           
    }

    public void MakeSoundObject(Sounds soundeffect)
    {
        GameObject soundObj = GameObject.Instantiate(SoundObjectPrefab);
        FmodPlayScript soundscript = soundObj.GetComponent<FmodPlayScript>();
        soundscript.PlaySound(SoundsDatabase[soundeffect]);
    }
}


