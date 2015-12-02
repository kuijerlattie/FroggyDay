using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spells : MonoBehaviour {

    public List<SpellInfo> spellslist;
    [SerializeField]
    GameObject hitballprefab;
    [SerializeField]
    Sprite[] spellIcons;
    [SerializeField]
    AudioClip[] spellSounds;
    [SerializeField]
    public AudioClip Cooldown;
    [SerializeField]
    public AudioClip lowOnMana;
    public AudioClip SpellNotLearned;
    public AudioClip OutOfCharges;
    [SerializeField]
    GameObject[] particlePrefabs;

    public void MakeSpellForward(Spells.SpellInfo spell, Transform caster, Vector3 forward)
    {
        Vector3 _forward = new Vector3(forward.x, 0, forward.z);
        Vector3 _right = new Vector3(-_forward.z, 0, _forward.x);
        Vector3 _up = caster.up;

        for (int i = 0; i < spell.hitballlist.Count; i++)
        {
            HitBall hitball = spell.hitballlist[i];

            GameObject _gameObject = GameObject.Instantiate(hitballprefab);
            _gameObject.transform.position =
            caster.position + hitball.position.x * _right + hitball.position.y * _up + hitball.position.z * _forward;
            _gameObject.transform.localScale = new Vector3(hitball.radius, hitball.radius, hitball.radius);
            HitboxScript hitboxscript = _gameObject.GetComponent<HitboxScript>();
            hitboxscript.velocity = hitball.velocity.x * _right + hitball.velocity.y * _up + hitball.velocity.z * _forward;

            //particles
            GameObject _particles = GameObject.Instantiate(particlePrefabs[hitball.particleID]);
            _particles.transform.LookAt(_particles.transform.position - hitboxscript.velocity);
            _particles.transform.parent = _gameObject.transform;
            _particles.transform.localPosition = Vector3.zero;

            

            hitboxscript.dmg = spell.dmg;
            hitboxscript.dmgot = spell.dmgot;
            hitboxscript.dmgottime = spell.dmgots;

            hitboxscript.slowpercentage = spell.slowpercentage;
            hitboxscript.slowseconds = spell.slowseconds;

            hitboxscript.stunseconds = spell.stunSeconds;

            if(spell.selfheal > 0)
            {
                GameObject.FindObjectOfType<PlayerScript>().health += spell.selfheal;
            }
            if (spell.selfmana > 0)
            {
                GameObject.FindObjectOfType<PlayerScript>().mana += spell.selfmana;
            }

            hitboxscript.layer = caster.gameObject.layer;
            hitboxscript.Spawn(hitball.spawndelay, hitball.duration);
        }
    }

    void Awake()
    {
        spellslist = new List<SpellInfo>();
        spellslist.Add(new SpellInfo(spellIcons[0], SoundManager.Sounds.FireB, 0, 0, "emptyspell", "enmptyspell", new HitBall[] { }, 0));

        spellslist.Add(new SpellInfo(spellIcons[1], SoundManager.Sounds.Spell1, 0, 0.8f, "Q", "description1",
            new HitBall[] {
                new HitBall(0, new Vector3(0,1,2), 0.0f, 1.0f, 3.0f, Vector3.forward * 100)
                //new HitBall(new Vector3(0,0,4), 0.4f, 1.0f, 3.0f, Vector3.zero),
               // new HitBall(new Vector3(0,0,6), 0.6f, 1.0f, 3.0f, Vector3.zero),
               // new HitBall(new Vector3(0,0,8), 0.8f, 1.0f, 3.0f, Vector3.zero),
               // new HitBall(new Vector3(0,0,10), 1.0f, 1.0f, 3.0f, Vector3.zero)
            }
            , 30,   //dmg
              0,     //dmg over time
              0,     //dmg over time seconds
              0,     //slow percentage
              0,     //slow seconds
              0));   //stun seconds

        spellslist.Add(new SpellInfo(spellIcons[2], SoundManager.Sounds.FireB, 5, 3, "W", "description1",
            new HitBall[] {
                new HitBall(0, new Vector3(0,0,5).normalized, 0.08f, 1.0f, 2.0f, Vector3.forward * 100),
                new HitBall(0, new Vector3(2.5f,0,2.5f).normalized, 0.04f, 1.0f, 2.0f, new Vector3(1,0,1).normalized * 100),
                new HitBall(0, new Vector3(-2.5f,0,2.5f).normalized, 0.12f, 1.0f, 2.0f, new Vector3(-1,0,1).normalized * 100),
                new HitBall(0, new Vector3(5,0,0).normalized, 0.00f, 1.0f, 2.0f, new Vector3(3,0,1).normalized * 100),
                new HitBall(0, new Vector3(-5,0,0).normalized, 0.16f, 1.0f, 2.0f, new Vector3(-3,0,1).normalized * 100),
                
                new HitBall(0, new Vector3(0,0,-5).normalized, 0.24f, 1.0f, 2.0f, -Vector3.forward * 100),
                new HitBall(0, new Vector3(2.5f,0,-2.5f).normalized, 0.28f, 1.0f, 2.0f, new Vector3(1,0,-1).normalized * 100),
                new HitBall(0, new Vector3(-2.5f,0,-2.5f).normalized, 0.20f, 1.0f, 2.0f, new Vector3(-1,0,-1).normalized * 100)
            }
            , 20, 0, 0, 0, 0));

        spellslist.Add(new SpellInfo(spellIcons[3], SoundManager.Sounds.IceB, 8, 5, "E", "description2",
         new HitBall[] {
                new HitBall(0, new Vector3(0,0,1.5f), 0.0f, 1.0f, 4.0f, Vector3.forward * 100),
                new HitBall(1, new Vector3(0,0,1.5f), 0.0f, 1.0f, 4.0f, (Vector3.forward + Vector3.right).normalized * 100),
                new HitBall(1, new Vector3(0,0,1.5f), 0.0f, 1.0f, 4.0f, (Vector3.forward - Vector3.right).normalized * 100)
         }
         , 30,   //dmg
         0,     //dmg over time
         0,     //dmg over time seconds
         35,     //slow percentage
         5,     //slow seconds
         0));   //stun seconds




        spellslist.Add(new SpellInfo(
            spellIcons[4],  //icon
            SoundManager.Sounds.WindS, //spell cast sound
            12,             //manacost
            8,              //cooldown seconds
            "R",       //spell name
            "description2", //spell description
            new HitBall[] {
                new HitBall(1, new Vector3(0,0,6), 0.0f, 0.5f, 4.0f, Vector3.zero),  //front
                new HitBall(1, new Vector3(0,0,12), 0.2f, 0.5f, 8.0f, Vector3.zero),  //front
                new HitBall(1, new Vector3(0,0,18), 0.4f, 0.5f, 12.0f, Vector3.zero),  //front
                   
         }
          , 30,   //dmg
          0,     //dmg over time
          0,     //dmg over time seconds
          0,     //slow percentage
          0,     //slow seconds
          5));   //stun seconds

        //melee attack only used by enemies
        spellslist.Add(new SpellInfo(spellIcons[5], SoundManager.Sounds.EMidAttack, 0, 3, "melee attack", "description melee",
         new HitBall[] {
                new HitBall(0, new Vector3(0,0,1), 0.0f, 0.5f, 3.0f, Vector3.zero)
         }
         , 15, 0, 0, 0, 0));
        spellslist.Add(new SpellInfo(spellIcons[6], SoundManager.Sounds.Heal, 0, 0, "Healing Potion", "Heal yourself for X amount of health", new HitBall[] { }, 0, 0, 0, 0, 0, 0, 10, 0, 0)); //hp potion
        spellslist.Add(new SpellInfo(spellIcons[7], SoundManager.Sounds.Mana, 0, 0, "Mana Potion", "Replenish x amount of your mana", new HitBall[] { }, 0, 0, 0, 0, 0, 0, 0, 10, 0)); //mana potion

    }


    public class HitBall
    {
        public Vector3 position;
        public float spawndelay;
        public float duration;
        public float radius;
        public Vector3 velocity;
        public int particleID;

        public HitBall(int pparticleID, Vector3 pposition, float pspawndelay, float pduration, float pradius, Vector3 pvelocity)
        {
            particleID = pparticleID;
            position = pposition;
            spawndelay = pspawndelay;
            duration = pduration;
            radius = pradius;
            velocity = pvelocity;
        }
    }



    public class SpellInfo
    {
        public Sprite icon;
        public string name;
        public string description;
        public int dmg;
        public int dmgot;
        public int dmgots;
        public float slowpercentage;
        public float slowseconds;
        public float stunSeconds;
        private float _cooldown;
        public float cooldown { get { return _cooldown; } }
        public int manacost;
        public SoundManager.Sounds soundeffect;
        public int selfheal;
        public int selfmana;
        public int uses;
        public bool learned;

       public  List<HitBall> hitballlist = new List<HitBall>();

       public SpellInfo(Sprite picon, SoundManager.Sounds psoundeffect, int pmanacost, float pcooldown, string pname, string pdestription, HitBall[] hitballs,
                         int pdmg, int pdmgovertime = 0, int pdmgovertimeseconds = 0, float pslowpercentage = 0, float pslowseconds = 0, float pstunseconds = 0, int pselfheal = 0, int pselfmana = 0, int puses = -1, bool plearned = true)
        {
            icon = picon;
            manacost = pmanacost;
            dmg = pdmg;
            dmgot = pdmgovertime;
            dmgots = pdmgovertimeseconds;
            _cooldown = pcooldown;
            name = pname;
            description = pdestription;
            slowpercentage = pslowpercentage;
            slowseconds = pslowseconds;
            stunSeconds = pstunseconds;
            hitballlist.AddRange(hitballs);
            soundeffect = psoundeffect;
            selfheal = pselfheal;
            selfmana = pselfmana;
            uses = puses;
            learned = plearned;     
            
        }
    }

}
