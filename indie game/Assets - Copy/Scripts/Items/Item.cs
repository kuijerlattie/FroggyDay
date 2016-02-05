using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Item{
    public stats owner;
    public Image image;
    public string name;
    public string description;
    public int buyPrice;
    public int sellPrice;
    protected GameObject model;

    public void Drop(Vector3 location)
    {
        //change this to corresponding prefab
        GameObject gameobject = GameObject.Instantiate(model);

        gameobject.transform.position = location;
        ItemPickup pickup = gameobject.AddComponent<ItemPickup>();
        pickup.item = this;
       
        pickup.gameobject = gameobject;
    }

    abstract public void Pickup();
    

    abstract public bool Use(stats user);

   


}
