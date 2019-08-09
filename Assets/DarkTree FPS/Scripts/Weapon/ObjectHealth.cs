/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;

namespace DarkTreeFPS
   {
    public class ObjectHealth : MonoBehaviour {

        //Blank class used for interaction between player damage and reciever

        public float health = 100;
        
        public bool instantiateAfterDeath = false;

        public GameObject objToInstantiate;

        void Update()
        {
            if (health < 0)
            {
                if(instantiateAfterDeath)
                    Instantiate(objToInstantiate, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }
    }
}
