using UnityEngine;
using System.Collections;

//put this on the scroll prefab, when instantiating the prefab use 'SetSpell' as well
public class Scroll : Item {

    private Spells _spell = Spells.None;

    public Scroll()
    {
        model = GameObject.FindObjectOfType<ItemManager>().scrollModel;
    }


    public void SetSpell(Spells spell)
    {
        _spell = spell;
    }
    public enum Spells
    {
        None,   //means this item will do nothing
        W = 8,  //upgrade spell ID
        E = 9,
        R = 10
    }

    public override void Pickup()
    {
        switch (_spell)
        {
            case Spells.W:
                GameObject.FindObjectOfType<PlayerScript>().wscroll = true;
                break;
            case Spells.E:
                GameObject.FindObjectOfType<PlayerScript>().escroll = true;
                break;
            case Spells.R:
                GameObject.FindObjectOfType<PlayerScript>().rscroll = true;
                break;
            default:
                break;
        }
        
    }

    public override bool Use(stats user)
    {
        return false;
    }
}
