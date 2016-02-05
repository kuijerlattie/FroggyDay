using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    public GameObject healthpotionModel;
    public GameObject manapotionModel;
    public GameObject chestModel;
    public GameObject scrollModel;
    public Vector3 WSCROLLPOS;
    public Vector3 ESCROLLPOS;
    public Vector3 RSCROLLPOS;

    void Start()
    {
        
            Scroll scroll = new Scroll();
            scroll.SetSpell(Scroll.Spells.W);
            scroll.Drop(WSCROLLPOS);

            Scroll scroll2 = new Scroll();
            scroll2.SetSpell(Scroll.Spells.E);
            scroll2.Drop(ESCROLLPOS);

            Scroll scroll3 = new Scroll();
            scroll3.SetSpell(Scroll.Spells.R);
            scroll3.Drop(RSCROLLPOS);
            
    }
}
