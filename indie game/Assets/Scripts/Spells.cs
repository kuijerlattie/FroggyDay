using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spells : MonoBehaviour {

    public List<SpellInfo> spellslist;
    [SerializeField]
    GameObject hitballprefab;
    [SerializeField]
    Texture2D icon1;

    public void MakeSpell(Spells.SpellInfo spell, Transform caster)
    {
        Vector3 _forward = caster.forward;
        Vector3 _right = caster.right;
        Vector3 _up = caster.up;
        LayerMask layermask = (1 << 11);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
        {
            if ((hit.point - caster.position).magnitude > 0.5f)
            {
                
                Vector3 tempforward = (hit.point - caster.position).normalized;
                _forward = new Vector3(tempforward.x, 0, tempforward.z);
                _right = new Vector3(-_forward.z, 0, _forward.x);
            }
        }
        for (int i = 0; i < spell.hitballlist.Count; i++) {
            HitBall hitball = spell.hitballlist[i];
        
            GameObject _gameObject = GameObject.Instantiate(hitballprefab);
            _gameObject.transform.position =
            caster.position + hitball.position.x * _right + hitball.position.y * _up + hitball.position.z * _forward;
            _gameObject.transform.localScale = new Vector3(hitball.radius, hitball.radius, hitball.radius);
            HitboxScript hitboxscript = _gameObject.GetComponent<HitboxScript>();
            hitboxscript.dmg = spell.dmg;
            hitboxscript.dmgot = spell.dmgot;
            hitboxscript.dmgottime = spell.dmgots;

            hitboxscript.slowpercentage = spell.slowpercentage;
            hitboxscript.slowseconds = spell.slowseconds;

            hitboxscript.layer = caster.gameObject.layer;
            hitboxscript.Spawn(hitball.spawndelay, hitball.duration);
        }
    }

    void Start()
    {
        spellslist = new List<SpellInfo>();

        spellslist.Add(new SpellInfo(icon1, 0, 3, "basic spell", "description1",
            new HitBall[] {
                new HitBall(new Vector3(0,0,2), 0.2f, 1.0f, 3.0f),
                new HitBall(new Vector3(0,0,4), 0.4f, 1.0f, 3.0f),
                new HitBall(new Vector3(0,0,6), 0.6f, 1.0f, 3.0f),
                new HitBall(new Vector3(0,0,8), 0.8f, 1.0f, 3.0f),
                new HitBall(new Vector3(0,0,10), 1.0f, 1.0f, 3.0f)
            }
            , 1, 0, 0, 50, 5));

        spellslist.Add(new SpellInfo(icon1, 10, 3, "spell1", "description1",
            new HitBall[] {
                new HitBall(new Vector3(0,0,8), 0.0f, 1.0f, 5.0f),
                new HitBall(new Vector3(3,0,7), 0.0f, 1.0f, 4.0f),
                new HitBall(new Vector3(-3,0,7), 0.0f, 1.0f, 4.0f),
                new HitBall(new Vector3(5,0,6), 0.0f, 1.0f, 3.0f),
                new HitBall(new Vector3(-5,0,6), 0.0f, 1.0f, 3.0f)
            }
            , 1, 0, 0, 0, 0));

        spellslist.Add(new SpellInfo(icon1, 10, 3, "spell2", "description2",
         new HitBall[] {
                new HitBall(new Vector3(0,0,8), 0.0f, 1.0f, 4.0f),

                new HitBall(new Vector3(5,0,5), 0.0f, 1.0f, 4.0f),
                new HitBall(new Vector3(-5,0,5), 0.0f, 1.0f, 4.0f),

                new HitBall(new Vector3(8,0,0), 0.0f, 1.0f, 4.0f),
                new HitBall(new Vector3(-8,0,0), 0.0f, 1.0f, 4.0f),

                new HitBall(new Vector3(5,0,-5), 0.0f, 1.0f, 4.0f),
                new HitBall(new Vector3(-5,0,-5), 0.0f, 1.0f, 4.0f),

                new HitBall(new Vector3(0,0,-8), 0.0f, 1.0f, 4.0f),
         }
         , 1, 0, 0, 0 ,0));

        spellslist.Add(new SpellInfo(icon1, 10, 3, "spell3", "description2",
         new HitBall[] {
                new HitBall(new Vector3(0,0,0), 0.0f, 1.0f, 25.0f)
         }
         , 1, 0, 0, 0, 0));

        spellslist.Add(new SpellInfo(icon1, 0, 3, "melee attack", "description melee",
         new HitBall[] {
                new HitBall(new Vector3(0,0,1), 0.0f, 0.5f, 3.0f)
         }
         , 1, 0, 0, 0, 0));

    }


    public class HitBall
    {
        public Vector3 position;
        public float spawndelay;
        public float duration;
        public float radius;

        public HitBall(Vector3 pposition, float pspawndelay, float pduration, float pradius)
        {
            position = pposition;
            spawndelay = pspawndelay;
            duration = pduration;
            radius = pradius;
        }
    }
    public class SpellInfo
    {
        public Texture2D icon;
        public string name;
        public string description;
        public int dmg;
        public int dmgot;
        public int dmgots;
        public float slowpercentage;
        public float slowseconds;
        private int _cooldown;
        public int cooldown { get { return _cooldown; } }
        public int manacost;

       public  List<HitBall> hitballlist = new List<HitBall>();

        public SpellInfo(Texture2D picon, int pmanacost, int pcooldown, string pname, string pdestription, HitBall[] hitballs,
                         int pdmg, int pdmgovertime = 0, int pdmgovertimeseconds = 0, float pslowpercentage = 0, float pslowseconds = 0)
        {
            icon = picon;
            manacost = pmanacost;
            dmg = pdmg;
            dmgot = pdmgovertime;
            dmgots = pdmgovertimeseconds;
            _cooldown = pcooldown;
            name = description;
            description = pdestription;
            slowpercentage = pslowpercentage;
            slowseconds = pslowseconds;
            hitballlist.AddRange(hitballs);
        }
    }







}
