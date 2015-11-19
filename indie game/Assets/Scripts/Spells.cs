using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spells : MonoBehaviour {

    List<SpellInfo> spellslist;

    void Start()
    {
        spellslist = new List<SpellInfo>();
        
    }


    class HitBall
    {
        public Vector3 position;
        public float spawndelay;
        public float duration;

        public HitBall(Vector3 pposition, float pspawndelay, float pduration)
        {
            position = pposition;
            spawndelay = pspawndelay;
            duration = pduration;
        }
    }
    class SpellInfo
    {
        string name;
        string description;
        private int _attack;
        public int attack { get { return _attack; } }
        private int _cooldown;
        public int cooldown { get { return _cooldown; } }

        List<HitBall> hitballlist = new List<HitBall>();

        public SpellInfo(int pattack, int pcooldown, string pname, string pdestription, HitBall[] hitballs)
        {
            _attack = pattack;
            _cooldown = pcooldown;
            name = description;
            description = pdestription;
            hitballlist.AddRange(hitballs);
        }
    }







}
