/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.UI;

namespace DarkTreeFPS
{
    public class InventoryInteraction : MonoBehaviour
    {
        public Item item;
        public Inventory inventory;
        public Text infoText;

        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        public void RemoveItem()
        {
            inventory.RemoveItem(item, false);
            this.gameObject.SetActive(false);
        }

        public void Useitem()
        {
            if (item.type == ItemType.consumable)
            {
                inventory.UseItem(item, false);
            }
            gameObject.SetActive(false);
        }
    }
}
