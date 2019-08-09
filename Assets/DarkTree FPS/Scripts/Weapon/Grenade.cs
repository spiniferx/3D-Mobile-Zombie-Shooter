using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkTreeFPS {

    public class Grenade : MonoBehaviour
    {
        public float explosionTimer;
        public float explosionForce;
        public float damageRadius;
        public float damage;

        public GameObject explosionEffects;

        Collider[] colliders;
        GameObject effects_temp;

        void OnEnable()
        {
            effects_temp = Instantiate(explosionEffects);
            effects_temp.SetActive(false);

            StartCoroutine(Timer(explosionTimer));
        }
        
        IEnumerator Timer(float explosionTimer)
        {
                yield return new WaitForSeconds(explosionTimer);
                print("Coroutine ended");
                Explosion();
        }

        void Explosion()
        {
            print("Explosion");

            colliders = Physics.OverlapSphere(transform.position, damageRadius);

            foreach (Collider collider in colliders)
            {
                /*
                if (collider.GetComponent<NPC>())
                {
                    collider.GetComponent<NPC>().GetHit((int)damage);
                }

                if(collider.GetComponent<ZombieNPC>())
                {
                    collider.GetComponent<ZombieNPC>().ApplyHit((int)damage);
                }*/

                if(collider.GetComponent<ObjectHealth>())
                {
                    collider.GetComponent<ObjectHealth>().health -= damage;
                }
               
                if(collider.GetComponent<PlayerStats>())
                {
                    collider.GetComponent<PlayerStats>().health -= (int)damage;
                }


                if (collider.GetComponent<Rigidbody>())
                {
                    collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, damageRadius);
                }
            }

            effects_temp.transform.position = transform.position;
            effects_temp.transform.rotation = transform.rotation;

            effects_temp.SetActive(true);

            Destroy(gameObject);
        }
    }
}
