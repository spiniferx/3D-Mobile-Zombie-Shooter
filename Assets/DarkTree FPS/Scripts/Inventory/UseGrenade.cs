using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DarkTreeFPS
{
    public class UseGrenade : MonoBehaviour
    {
        Item item;
        WeaponManager weaponManager;

        UnityAction useGrenade;

        void Start()
        {
            weaponManager = FindObjectOfType<WeaponManager>();

            useGrenade += UseGrenadeAction;

            item = GetComponent<Item>();

            item.onUseEvent.AddListener(useGrenade);
        }

        public void UseGrenadeAction()
        {
            weaponManager.ShowGrenade();
        }
        
    }
}