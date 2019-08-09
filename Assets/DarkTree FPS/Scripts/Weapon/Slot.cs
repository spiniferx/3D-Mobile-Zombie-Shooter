/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;

namespace DarkTreeFPS {

    public class Slot : MonoBehaviour {
        
        public Weapon storedWeapon;
        public GameObject storedDropObject;

        public bool IsFree()
        {
            if (!storedWeapon)
                return true;
            else
                return false;
        }
    }
}
