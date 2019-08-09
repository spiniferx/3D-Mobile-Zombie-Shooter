/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DarkTreeFPS
{
    public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector]
        public Item item;

        private Image spriteImage;
        private UIItem selectedItem;
        private InventoryInteraction interaction;
        private Text ammoText;
        private Text infoText;

        float lastClick;
        float interval = 0.4f;

        private void Awake()
        {
            spriteImage = GetComponent<Image>();
            UpdateItem(null);
            selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
            infoText = GameObject.Find("InfoText").GetComponent<Text>();
            interaction = FindObjectOfType<InventoryInteraction>();
            ammoText = GetComponentInChildren<Text>();
        }

        public void UpdateItem(Item item)
        {
            this.item = item;
            if (this.item != null)
            {
                spriteImage.color = Color.white;
                spriteImage.sprite = this.item.icon;
            }
            else
            {
                spriteImage.color = Color.clear;
            }
        }

        void Update()
        {
            if (item == null)
            {
                if (ammoText != null)
                    ammoText.text = "";
                return;
            }

            if (item.type == ItemType.ammo)
            {
                if (ammoText != null)
                    ammoText.text = item.ammo.ToString();
            }
            else
            {
                if (ammoText != null)
                    ammoText.text = "";
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.item != null)
            {
                    if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        interaction.item = item;
                        interaction.transform.position = eventData.position;
                        interaction.gameObject.SetActive(true);
                    }
                    else
                    {
                        interaction.gameObject.SetActive(false);

                        if (selectedItem.item != null)
                        {
                            Item clone = new Item(selectedItem.item);
                            selectedItem.UpdateItem(this.item);
                            UpdateItem(clone);
                        }
                        else
                        {
                            selectedItem.UpdateItem(this.item);
                            UpdateItem(null);
                        }
                    }
                
            }
            else if (selectedItem.item != null)
            {
                UpdateItem(selectedItem.item);
                selectedItem.UpdateItem(null);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (item != null)
                infoText.text = item.description;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            infoText.text = " ";
        }
    }
}
