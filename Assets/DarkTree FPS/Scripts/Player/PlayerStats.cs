/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DarkTreeFPS
{
    /// <summary>
    /// It's very simple class so I think that there is no need in deep explanation of it
    /// When health less than or equal to zero -> Death() || if respawn -> Respawn()
    /// Death and Respawn methods works in same way but in oposite direction
    /// </summary>

    public class PlayerStats : MonoBehaviour
    {
        [Header("Health")]
        [Tooltip("Player's health")]
        public int health = 100;
        [Tooltip("UI element to draw health as number")]
        public Text healthUIText;
        [Tooltip("UI element to draw health as slider")]
        public Slider healthUISlider;

        [Header("Damage effect")]
        [Tooltip("UI Image with fullscreen hit fx")]
        public Image damageScreenFX;
        [Tooltip("UI Image color to change on hit")]
        public Color damageScreenColor;
        [Tooltip("UI Image fade speed after hit")]
        public float damageScreenFadeSpeed = 1.4f;

        [Header("Consume stats")]
        public bool useConsumeSystem = true;
        
        public int hydratation = 100;
        public float hydratationSubstractionRate = 3f;
        public int thirstDamage = 1;
        private float hydratationTimer;

        public int satiety = 100;
        public float satietySubstractionRate = 5f;
        public int hungerDamage = 1;
        private float satietyTimer;

        public Text playerStats;

        private Color damageScreenColor_temp;

        public static bool isPlayerDead = false;

        #region utility objects
        private Rigidbody playerRigidbody;
        private FPSController controller;
        private CapsuleCollider playerCollider;
        private Sway sway;

        private GameObject mainCameraGameobject;
        private Vector3 mainCameraStartPos;

        private WeaponManager weaponManager;

        //Don't create any rigidbody here
        private Rigidbody rigidbody_temp;
        #endregion
        
        private void Start()
        {
            isPlayerDead = false;

            playerRigidbody = GetComponent<Rigidbody>();
            controller = GetComponent<FPSController>();
            playerCollider = GetComponent<CapsuleCollider>();
            weaponManager = FindObjectOfType<WeaponManager>();
            sway = FindObjectOfType<Sway>();

            mainCameraGameobject = Camera.main.gameObject;
            mainCameraStartPos = mainCameraGameobject.transform.position;
        }
        
        void Update()
        {
            if (isPlayerDead)
            {
                if (mainCameraGameobject.transform.eulerAngles.x >= 90 || mainCameraGameobject.transform.eulerAngles.x <= -90 || mainCameraGameobject.transform.eulerAngles.z >= 90 || mainCameraGameobject.transform.eulerAngles.z <= -90)
                {
                    if (rigidbody_temp)
                        rigidbody_temp.constraints = RigidbodyConstraints.FreezeRotation;
                }
            }

            if (health <= 0 && !isPlayerDead)
            {
                PlayerDeath();
            }

            if (health < 0)
            {
                health = 0;
            }

            if(health >  100)
            {
                health = 100;
            }

            ConsumableManager(useConsumeSystem);
            DrawHealthStats();
            DrawPlayerStats();
        }
        
        public void ConsumableManager(bool useSystem)
        {
            if (!useSystem)
                return;

            if (Time.time > satietyTimer + satietySubstractionRate)
            {
                if (satiety <= 0)
                {
                    satiety = 0;
                    health -= hungerDamage;
                }

                satiety -= 1;
                satietyTimer = Time.time;
                
            }

            if (Time.time > hydratationTimer + hydratationSubstractionRate)
            {
                if (hydratation <= 0)
                {
                    hydratation = 0;
                    health -= thirstDamage;
                }
                hydratation -= 1;
                hydratationTimer = Time.time;
            }

            if(hydratation > 100)
            {
                hydratation = 100;
            }
            if(satiety > 100)
            {
                satiety = 100;
            }
        }

        public void DrawPlayerStats()
        {
            if(playerStats != null)
                playerStats.text = string.Format("--- Player statistic ---\n\n\n - Health: {0}\n\n - Hydratation: {1}\n\n - Satiety: {2}\n\n", health, hydratation, satiety);
        }

        public void ApplyDamage(int damage)
        {
            if (damage > 0)
            {
                health -= damage;
                damageScreenFX.color = damageScreenColor;
                damageScreenColor_temp = damageScreenColor;
                StartCoroutine(HitFX());
            }
        }

        public void AddSatiety(int points)
        {
            satiety += points;
        }

        public void AddHydratation(int points)
        {
            hydratation += points;
        }

        public void AddHealth(int hp)
        {
            health += hp;
        }

        IEnumerator HitFX()
        {
            while (damageScreenFX.color.a > 0)
            {
                damageScreenColor_temp = new Color(damageScreenColor_temp.r, damageScreenColor_temp.g, damageScreenColor_temp.b, damageScreenColor_temp.a -= damageScreenFadeSpeed * Time.deltaTime);
                damageScreenFX.color = damageScreenColor_temp;

                yield return new WaitForEndOfFrame();
            }
        }

        public void PlayerDeath()
        {
            if (!isPlayerDead)
            {
                controller.enabled = false;
                playerCollider.enabled = false;
                playerRigidbody.isKinematic = true;

                mainCameraGameobject.transform.parent = null;

                if (!rigidbody_temp)
                {
                    mainCameraGameobject.AddComponent<SphereCollider>();
                    mainCameraGameobject.GetComponent<SphereCollider>().radius = 0.2f;
                    rigidbody_temp = mainCameraGameobject.AddComponent<Rigidbody>();
                    rigidbody_temp.mass = 20;
                }
                else
                {
                    mainCameraGameobject.GetComponent<SphereCollider>().enabled = true;
                }

                rigidbody_temp.isKinematic = false;
                rigidbody_temp.AddTorque(mainCameraGameobject.transform.forward * 10, ForceMode.Impulse);

                rigidbody_temp.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                weaponManager.DropAllWeapons();

                sway.enabled = false;

                isPlayerDead = true;
            }
            else
                return;
        }

        public void Respawn()
        {
            health = 100;

            isPlayerDead = false;
            controller.enabled = true;
            playerCollider.enabled = true;
            playerRigidbody.isKinematic = false;

            mainCameraGameobject.transform.parent = this.transform;
            mainCameraGameobject.transform.position = mainCameraStartPos;
            mainCameraGameobject.transform.rotation = Quaternion.identity;
            mainCameraGameobject.GetComponent<SphereCollider>().enabled = false;


            rigidbody_temp.isKinematic = true;

            rigidbody_temp.constraints = RigidbodyConstraints.None;

            sway.enabled = true;

            weaponManager.UnhideWeaponOnRespawn();
        }

        void DrawHealthStats()
        {
            if (healthUIText != null)
                healthUIText.text = health.ToString();

            if (healthUISlider != null)
                healthUISlider.value = health;
        }
    }

}