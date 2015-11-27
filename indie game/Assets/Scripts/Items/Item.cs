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

    public void Drop(Vector3 location)
    {
        GameObject gameobject = (GameObject)GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        gameobject.transform.position = location;
        ItemPickup pickup = gameobject.AddComponent<ItemPickup>();
        pickup.item = this;
        pickup.gameobject = gameobject;
    }

    abstract public void Pickup();
    

    abstract public bool Use(stats user);

   


}
