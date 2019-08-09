/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;

namespace DarkTreeFPS {
    
    public class LadderTrigger : MonoBehaviour {

        private FPSController controller;

        private void Start()
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                controller.isClimbing = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                controller.isClimbing = false;
            }
        }
    }
}