/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;

namespace DarkTreeFPS
{
    public class GetCollisionTag : MonoBehaviour
    {
        public string contactTag;

        private void Update()
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
            {
                contactTag = hit.collider.tag;
            }
        }
    }
}