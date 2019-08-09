/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using UnityEngine.AI;

namespace DarkTreeFPS
{
    public class FootstepSound : MonoBehaviour
    {
        private AudioSource audioSource;

        public AudioClip[] woodSteps;
        public AudioClip[] dirtSteps;
        public AudioClip[] concreteSteps;
        public AudioClip[] metalSteps;

        GetCollisionTag collisionTag;

        public bool usedByNPC = false;

        // NPC fields
        [Header("Use this fields only for NPC")]
        private NavMeshAgent agent;
        public AudioSource audioSourceNPC;

        private void Start()
        {
            if (!usedByNPC)
            {
                audioSource = GetComponent<AudioSource>();
                collisionTag = GetComponentInParent<GetCollisionTag>();
            }

            if (usedByNPC)
            {
                agent = GetComponentInParent<NavMeshAgent>();
                collisionTag = GetComponent<GetCollisionTag>();
            }
        }

        public void PlayFootstep()
        {
            if (!usedByNPC)
            {
                audioSource.pitch = Random.Range(0.8f, 1f);

                switch (collisionTag.contactTag)
                {
                    case "Dirt":
                        audioSource.PlayOneShot(dirtSteps[Random.Range(0, dirtSteps.Length)]);
                        break;
                    case "Wood":
                        audioSource.PlayOneShot(woodSteps[Random.Range(0, woodSteps.Length)]);
                        break;
                    case "Concrete":
                        audioSource.PlayOneShot(concreteSteps[Random.Range(0, concreteSteps.Length)]);
                        break;
                    case "Metal":
                        audioSource.PlayOneShot(metalSteps[Random.Range(0, metalSteps.Length)]);
                        break;
                    default:
                        audioSource.PlayOneShot(dirtSteps[Random.Range(0, dirtSteps.Length)]);
                        break;
                }
            }

            if(usedByNPC)
            {
               audioSourceNPC.volume = agent.desiredVelocity.magnitude;

                audioSourceNPC.pitch = Random.Range(0.8f, 1f);

                switch (collisionTag.contactTag)
                {
                    case "Dirt":
                        audioSourceNPC.PlayOneShot(dirtSteps[Random.Range(0, dirtSteps.Length)]);
                        break;
                    case "Wood":
                        audioSourceNPC.PlayOneShot(woodSteps[Random.Range(0, woodSteps.Length)]);
                        break;
                    case "Concrete":
                        audioSourceNPC.PlayOneShot(concreteSteps[Random.Range(0, concreteSteps.Length)]);
                        break;
                    case "Metal":
                        audioSourceNPC.PlayOneShot(metalSteps[Random.Range(0, metalSteps.Length)]);
                        break;
                    default:
                        audioSourceNPC.PlayOneShot(dirtSteps[Random.Range(0, dirtSteps.Length)]);
                        break;
                }
            }
        }
    }
}
