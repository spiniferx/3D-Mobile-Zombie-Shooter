/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.Events;
using DarkTreeFPS;
using UnityStandardAssets.ImageEffects;

namespace DarkTreeFPS
{
    public class InventoryManager : MonoBehaviour
    {
        [System.Serializable]
        public class OnInventoryOpen : UnityEvent { }
        [System.Serializable]
        public class OnInventoryClose : UnityEvent { }

        Canvas canvas;
        FPSController controller;
        Blur blurEffect;
        InputManager input;

        public static bool showInventory = false;
        public bool isOpen = true;

        public OnInventoryOpen OnOpen;
        public OnInventoryClose OnClose;

        private void Start()
        {
            canvas = GetComponent<Canvas>();
            controller = FindObjectOfType<FPSController>();
            blurEffect = Camera.main.GetComponent<Blur>();
            input = FindObjectOfType<InputManager>();

            InventoryClose();
        }

        private void Update()
        {
            if (Input.GetKeyDown(input.Inventory) && !PlayerStats.isPlayerDead)
            {
                showInventory = !showInventory;
            }

            if (showInventory)
            {
                InventoryOpen();
            }
            else
            {
                InventoryClose();
            }
        }

        private void InventoryOpen()
        {
            if (isOpen)
                return;
            else
            {
                canvas.enabled = true;
                controller.lockCursor = false;
                blurEffect.enabled = true;
                OnOpen.Invoke();
                Time.timeScale = 0;
                isOpen = true;

            }
        }
        private void InventoryClose()
        {
            if (!isOpen)
                return;
            else
            {
                canvas.enabled = false;
                controller.lockCursor = true;
                blurEffect.enabled = false;
                OnClose.Invoke();
                Time.timeScale = 1;
                isOpen = false;
            }
        }

        public void MobileToggle()
        {
            showInventory = !showInventory;
        }
    }
}