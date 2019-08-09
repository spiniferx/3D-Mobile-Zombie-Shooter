using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType {none, weapon, ammo, consumable }

public class Item : MonoBehaviour
{
    [System.Serializable]
    public class OnUseEvent : UnityEvent { }
    [System.Serializable]
    public class OnPickupEvent : UnityEvent { }

    public int id;
    public string title;
    public string description;
    public ItemType type;
    public Sprite icon;

    public int ammo;
    
    [SerializeField]
    public OnUseEvent onUseEvent;
    [SerializeField]
    public OnPickupEvent onPickupEvent;

    public Item(int id, string title, string description, ItemType itemType)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = icon;
        this.type = itemType;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
        this.type = item.type;
    }
}
