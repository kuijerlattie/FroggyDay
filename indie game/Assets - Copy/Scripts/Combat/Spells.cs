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

            //hitballs can give immunity to the caster until the hitball is destroyed
            hitboxscript.giveImmunity = hitball.immuneCaster;
            if (hitball.setParent)
            {
                hitboxscript.followTarget = caster;
            }

            //particles
            if (hitball.particleID != -1)
            {
                GameObject _particles = GameObject.Instantiate(particlePrefabs[hitball.particleID]);
                _particles.transform.LookAt(_particles.transform.position - hitboxscript.velocity);
                _particles.transform.parent = _gameObject.transform;
                _particles.transform.localPosition = Vector3.zero;
            }

            

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

        //this empty spell is only used as a placeholder at the start of the game, before other spells are unlocked
        spellslist.Add(new SpellInfo(spellIcons[0], SoundManager.Sounds.FireB, 0, 0, "emptyspell", "enmptyspell", new HitBall[] { }, 0));

        spellslist.Add(new SpellInfo(spellIcons[1], SoundManager.Sounds.Spell1, 0, 0.8f, "Q", "description1",
            new HitBall[] {
                new HitBall(0, new Vector3(0,3.5f,2), 0.0f, 1.0f, 5.0f, Vector3.forward * 100)
            }
            , 30,   //dmg
              0,     //dmg over time
              0,     //dmg over time seconds
              0,     //slow percentage
              0,     //slow seconds
              0));   //stun seconds

        spellslist.Add(new SpellInfo(spellIcons[2], SoundManager.Sounds.FireB, 5, 3, "W", "description1",
             new HitBall[] {
                new HitBall(1, new Vector3(0,3.5f,2), 0.0f, 1.0f, 5.0f, Vector3.forward * 100)
            }
            , 30,   //dmg
              0,     //dmg over time
              0,     //dmg over time seconds
              0,     //slow percentage
              0,     //slow seconds
              0));   //stun seconds

        spellslist.Add(new SpellInfo(spellIcons[3], SoundManager.Sounds.IceB, 8, 5, "E", "description2",
        new HitBall[] {
                new HitBall(2, new Vector3(0,3.5f,2), 0.0f, 1.0f, 5.0f, Vector3.forward * 100)
            }
            , 30,   //dmg
              0,     //dmg over time
              0,     //dmg over time seconds
              0,     //slow percentage
              0,     //slow seconds
              0));   //stun seconds




        spellslist.Add(new SpellInfo(
            spellIcons[4],  //icon
            SoundManager.Sounds.WindS, //spell cast sound
            12,             //manacost
            8,              //cooldown seconds
            "R",       //spell name
            "description2", //spell description
            new HitBall[] {
                new HitBall(3, new Vector3(0,3.5f,2), 0.0f, 1.0f, 5.0f, Vector3.forward * 100)
            }
            , 30,   //dmg
              0,     //dmg over time
              0,     //dmg over time seconds
              0,     //slow percentage
              0,     //slow seconds
              0));   //stun seconds

        //melee attack only used by enemies
        spellslist.Add(new SpellInfo(spellIcons[5], SoundManager.Sounds.EMidAttack, 0, 3, "melee attack", "description melee",
         new HitBall[] {
                new HitBall(0, new Vector3(0,0,1), 0.0f, 0.5f, 3.0f, Vector3.zero)
         }
         , 15, 0, 0, 0, 0));
        spellslist.Add(new SpellInfo(spellIcons[6], SoundManager.Sounds.Heal, 0, 0, "Healing Potion", "Heal yourself for X amount of health", new HitBall[] { new HitBall(4, Vector3.zero, 0, 1, 1, Vector3.zero, true) }, 0, 0, 0, 0, 0, 0, 10, 0, 0)); //hp potion
        spellslist.Add(new SpellInfo(spellIcons[7], SoundManager.Sounds.Mana, 0, 0, "Mana Potion", "Replenish x amount of your mana", new HitBall[] { new HitBall(5, Vector3.zero, 0, 1, 1, Vector3.zero, true) }, 0, 0, 0, 0, 0, 0, 0, 10, 0)); //mana potion


        //Spells dat have a player-following effect
        spellslist.Add(new SpellInfo(spellIcons[2], SoundManager.Sounds.FireB, 40, 15, "spell id 8", "big AoE fire attack, upgraded W",
         new HitBall[] {
                new HitBall(6, new Vector3(0,0,0), 0.0f, 5.0f, 1.0f, Vector3.zero, true),

                new HitBall(-1, new Vector3(0,0,0), 1.0f, 0.2f, 25.0f, Vector3.zero, true),
                new HitBall(-1, new Vector3(0,0,0), 2.0f, 0.2f, 25.0f, Vector3.zero, true),
                new HitBall(-1, new Vector3(0,0,0), 3.0f, 0.2f, 25.0f, Vector3.zero, true),
                new HitBall(-1, new Vector3(0,0,0), 4.0f, 0.2f, 25.0f, Vector3.zero, true)
         }
         , 10, 0, 0, 0, 0));

        spellslist.Add(new SpellInfo(spellIcons[2], SoundManager.Sounds.FireB, 40, 15, "spell id 9", "shield, upgraded E",
         new HitBall[] {
                new HitBall(7, new Vector3(0,0,0), 0.0f, 5.0f, 1.0f, Vector3.zero, true, true)
         }
         , 0, 0, 0, 0, 0));

        //TODO
        spellslist.Add(new SpellInfo(spellIcons[2], SoundManager.Sounds.FireB, 40, 15, "spell id 10", "bbb, upgraded R",
         new HitBall[] {
                new HitBall(8, new Vector3(0,0,0), 0.0f, 5.0f, 1.0f, Vector3.zero, true, true)
         }
         , 0, 0, 0, 0, 0));

 

    }

    /// <summary>
    /// Hitballs are the building blocks of spells, they contain almost all the spell info
    /// </summary>
    public class HitBall
    {
        public Vector3 position;
        public float spawndelay;
        public float duration;
        public float radius;
        public Vector3 velocity;
        public int particleID;
        public bool setParent;
        public bool immuneCaster;

        public HitBall(int pparticleID, Vector3 pposition, float pspawndelay, float pduration, float pradius, Vector3 pvelocity, bool setParent = false, bool immuneCaster = false)
        {
            particleID = pparticleID;
            position = pposition;
            spawndelay = pspawndelay;
            duration = pduration;
            radius = pradius < 5? 5 : pradius;
            velocity = pvelocity;
            this.setParent = setParent;
            this.immuneCaster = immuneCaster;
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
