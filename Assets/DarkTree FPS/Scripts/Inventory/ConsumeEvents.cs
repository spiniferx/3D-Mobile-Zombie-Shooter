/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class automaticly creates listener for item use event.
/// When we will consume some item we will use reciver method from that script
/// </summary>

namespace DarkTreeFPS {

    public class ConsumeEvents : MonoBehaviour
    {
        public enum ConsumableEvents { addHealth, addSatiety, addHydratation }

        [Header("What reference should I add?")]
        public ConsumableEvents m_Event;

        public int pointsToAdd;

        private PlayerStats playerStats;
        private Item item;

        UnityAction addHealth, addSatiety, addHydratation;

        private void Start()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            item = GetComponent<Item>();

            addHealth += AddHealth;
            addHydratation += AddHydratation;
            addSatiety += AddSatiety;

            switch (m_Event)
            {
                case ConsumableEvents.addHealth:
                    item.onUseEvent.AddListener(addHealth);
                    break;
                case ConsumableEvents.addHydratation:
                    item.onUseEvent.AddListener(addHydratation);
                    break;
                case ConsumableEvents.addSatiety:
                    item.onUseEvent.AddListener(addSatiety);
                    break;
            }
        }

        public void AddHealth()
        {
            playerStats.AddHealth(pointsToAdd);
        }

        public void AddSatiety()
        {
            playerStats.AddSatiety(pointsToAdd);
        }

        public void AddHydratation()
        {
            playerStats.AddHydratation(pointsToAdd);
        }


    }
}
