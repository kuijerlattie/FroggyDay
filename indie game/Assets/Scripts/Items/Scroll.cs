using UnityEngine;
using System.Collections;

//put this on the scroll prefab, when instantiating the prefab use 'SetSpell' as well
public class Scroll : Item {

    private Spells _spell = Spells.None;

    public void SetSpell(Spells spell)
    {
        _spell = spell;
    }
    public enum Spells
    {
        None,   //means this item will do nothing
        Q,
        W,
        E,
        R
    }

    public override void Pickup()
    {

    }

    public override bool Use(stats user)
    {
    
        return false;
    }
}
