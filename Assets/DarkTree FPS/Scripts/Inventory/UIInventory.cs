/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using System.Collections.Generic;
using UnityEngine;

namespace DarkTreeFPS
{
    public class UIInventory : MonoBehaviour
    {
        public List<UIItem> UIItems = new List<UIItem>();
        public GameObject slotPrefab;
        public Transform slotPanel;


        public int numberOfSlots = 16;

        private void Awake()
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                GameObject instance = Instantiate(slotPrefab);
                instance.transform.SetParent(slotPanel);
                UIItems.Add(instance.GetComponentInChildren<UIItem>());
            }
        }

        public void UpdateSlot(int slot, Item item)
        {
            UIItems[slot].UpdateItem(item);
        }

        public void AddNewItem(Item item)
        {
            UpdateSlot(UIItems.FindIndex(i => i.item == null), item);
        }

        public void RemoveItem(Item item)
        {
            UpdateSlot(UIItems.FindIndex(i => i.item == item), null);
        }
    }
}
