/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;

namespace DarkTreeFPS
{
    public class WeaponPickup : MonoBehaviour
    {
        private WeaponManager weaponManager;
        
        public int ammoInWeaponCount;
        public string weaponNameToEquip;

        private void Start()
        {
            weaponManager = FindObjectOfType<WeaponManager>();
        }

        public void Pickup()
        {
                    weaponManager.EquipWeapon(weaponNameToEquip, gameObject);
                    print("Pickup:" + weaponNameToEquip);
            
        }
    }
}
