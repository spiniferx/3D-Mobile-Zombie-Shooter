/// DarkTreeDevelopment (2019) DarkTree FPS v1.1
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!
using UnityEngine;

namespace DarkTreeFPS
{
    public enum WeaponType { SMG, SniperRiffle, Pistol, Shotgun, Melee, Grenade}

    /// <summary>
    /// Here is a basic class for scriptable object file with the all settings of weapon
    /// It's used by Weapon.cs to get a values on Start()
    /// </summary>

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon Data", order = 0)]
    public class WeaponSettingSO : ScriptableObject
    {
        public string weaponName;
        public WeaponType weaponType;

        public Sprite weaponIcon;

        [Header("Settings of objects")]

        [Tooltip("Particle system which being started after calling fire method")]
        public ParticleSystem MuzzleFlashParticlesFX;
        [Tooltip("SFX for each type of weapon state")]
        public AudioClip shotSFX, reloadingSFX, emptySFX;

        [Tooltip("Shell object which drops from the defined point in Weapon() class after calling fire method")]
        public GameObject shell;
        [Tooltip("Maximal amount of shell which being created on Start()")]
        public int shellsPoolSize;
        [Tooltip("Force with shells ejected from weapon")]
        public float shellEjectingForce;

        [Header("Weapon stats")]

        [Tooltip("Maximal and minimal damage ammounts to apply on target")]
        public int damageMinimum;
        public int damageMaximum;
        
        [Tooltip("Force that be applyed to rigidbody on raycast hit")]
        public float rigidBodyHitForce;
        [Tooltip("Time in seconds to call next fire. 1 means 1 shot per second, 0.5 means 2 shots per second etc")]
        public float fireRate;
        [Tooltip("Bullet spread value. Influence on shooting trajectory in static reticle mod")]
        public float spread = 0.01f;
        [Tooltip("Recoil value to X axis of camera")]
        public Vector3 recoil;

        [Header("Scope settings")]

        [Tooltip("Can weapon use scope when aiming?")]
        public bool canUseScope;
        [Tooltip("Zoom FOV when scope is active")]
        public float scopeFOV;
        [Tooltip("Input sensetivity when player aiming")]
        public float scopeSensitivityX;
        [Tooltip("Input sensetivity when player aiming")]
        public float scopeSensitivityY;
        
        [Header("Melee settings (for melee only!)")]
        public float meleeAttackDistance;
        public float meleeAttackRate = 0.4f;
        public int meleeDamagePoints;
        public float meleeRigidbodyHitForce;
        public float meleeHitTime;
        public AudioClip meleeHitFX;

        [Header("Ballistic settings")]
        [Tooltip("Initial bullet velocity in meters per second. Recomended to take real weapons parameters")]
        public float bulletInitialVelocity = 360;
        [Tooltip("Air resistance force that slow down bullet in air. Default value is 1. If 0 no air resistance applied to bullet")]
        public float airResistanceForce = 1;
        [Tooltip("Projectile prefab used as projectile. Select one from prefabs")]
        public GameObject projectile;
        [Tooltip("Max amount of projectiles which being created on Start()")]
        [Range(1, 100)]
        public int projectilePoolSize = 1;

        [Header("Grenade settings")]
        public GameObject grenadePrefab;
        public float throwForce = 1500;
    }
}
