using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour {

    private int _health;
    public int health { get { return _health; } set { _health = value > 0 ? (value <= maxhealth? value : maxhealth) : 0; } }
    public int maxhealth;

    private int _mana;
    public int mana { get { return _mana; } set { _mana = value > 0 ? (value <= maxmana ? value : maxmana) : 0; } }
    public int maxmana;


    /*
    int attack { get; set; }
    int defense { get; set; }
    int movespeed { get; set; }
    int attackspeed { get; set; }
    */
}
