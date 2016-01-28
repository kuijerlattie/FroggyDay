using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollTradeSCript : MonoBehaviour {

    public GameObject buymenu;
    PlayerScript pscript;
    Spells spellmanager;

    public Button[] Buttons;
    
    const int HEALTH_PRICE = 10;
    const int MANA_PRICE = 10;
    const int UPGRADE_W_PRICE = 10;
    const int UPGRADE_E_PRICE = 10;
    const int UPGRADE_R_PRICE = 10;
    const int CHEST_GOLD = 25;

    bool boughtW = false;
    bool boughtE = false;
    bool boughtR = false;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8) //player
        {
            OpenShop();
            UseScrolls();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            CloseShop();
        }
    }

    private bool CheckBuy(int price)
    {
        if (pscript == null) pscript = GameObject.FindObjectOfType<PlayerScript>();
        if (spellmanager == null) spellmanager = GameObject.FindObjectOfType<Spells>();

        if (pscript.gold >= price)
            return true;
        return false;
    }

    private bool Buy(int price)
    {
        bool enoughgold = CheckBuy(price);
        if (enoughgold) pscript.gold -= price;
        RefreshButtons();
        return enoughgold;
    }

    private void RefreshButtons()   //to make the buttons gray if not enough gold
    {
        if (CheckBuy(MANA_PRICE))
        {
            Buttons[0].image.color = Color.white;
        }
        else
        {
            Buttons[0].image.color = Color.gray;
        }

        if (CheckBuy(HEALTH_PRICE))
        {
            Buttons[1].image.color = Color.white;
        }
        else
        {
            Buttons[1].image.color = Color.gray;
        }

        if (CheckBuy(UPGRADE_W_PRICE) && !boughtW)
        {
            Buttons[2].image.color = Color.white;
        }
        else
        {
            Buttons[2].image.color = Color.gray;
        }

        if (CheckBuy(UPGRADE_E_PRICE) && !boughtE)
        {
            Buttons[3].image.color = Color.white;
        }
        else
        {
            Buttons[3].image.color = Color.gray;
        }

        if (!boughtR && CheckBuy(UPGRADE_R_PRICE) )
        {
            Debug.Log(UPGRADE_R_PRICE);
            Buttons[4].image.color = Color.white;
        }
        else
        {
            Buttons[4].image.color = Color.gray;
        }
        if(pscript.Chests >= 1)
        {
            Buttons[5].image.color = Color.white;
        }
        else
        {
            Buttons[5].image.color = Color.gray;
        }
    }

    public void OpenShop()
    {
        buymenu.SetActive(true);
        RefreshButtons();
    }
    public void CloseShop()
    {
        buymenu.SetActive(false);
    }

    public void BuyHealth()
    {
        if (!Buy(HEALTH_PRICE)) return;
        spellmanager.spellslist[6].uses++;
        RefreshButtons();
    }
    public void BuyMana()
    {
        if (!Buy(MANA_PRICE)) return;
        spellmanager.spellslist[7].uses++;
        RefreshButtons();
    }
    public void BuyUpgradeW()
    {
        if (boughtW) return;
        if (!Buy(UPGRADE_W_PRICE)) return;
        pscript.spellFireSpecial = 8;
        boughtW = true;
        RefreshButtons();
    }
    public void BuyUpgradeE()
    {
        if (boughtE) return;
        if (!Buy(UPGRADE_E_PRICE)) return;
        pscript.spellIceSpecial = 9;
        boughtE = true;
        RefreshButtons();
    }
    public void BuyUpgradeR()
    {
        if (boughtR) return;
        if (!Buy(UPGRADE_R_PRICE)) return;
        pscript.spellAirSpecial = 10;
        boughtR = true;
        RefreshButtons();
    }

    public void SellChest()
    {
        pscript = GameObject.FindObjectOfType<PlayerScript>();
        if (pscript.Chests >= 1)
        {
            pscript.Chests--;
            pscript.gold += CHEST_GOLD;
        }
        RefreshButtons();
    }

    void UseScrolls()
    {
        PlayerScript pscript = GameObject.FindObjectOfType<PlayerScript>();

        if(pscript.wscroll)
        {
            pscript.spellFireNormal = (int)Scroll.Spells.W;
            pscript.SetSpell(PlayerScript.SpellSlots.spellW, pscript.spellFireNormal);
            pscript.wscroll = false;
        }
        if(pscript.escroll)
        {
            pscript.spellIceNormal = (int)Scroll.Spells.E;
            pscript.SetSpell(PlayerScript.SpellSlots.spellE, pscript.spellIceNormal);
            pscript.escroll = false;
        }
        if(pscript.rscroll)
        {
            pscript.spellAirNormal = (int)Scroll.Spells.R;
            pscript.SetSpell(PlayerScript.SpellSlots.spellR, pscript.spellAirNormal);
            pscript.rscroll = false;
        }
    }
}
