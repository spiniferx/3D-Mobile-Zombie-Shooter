using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DarkTreeFPS
{
    [CustomEditor(typeof(WeaponSettingSO))]
    public class WeaponDataCustomInspector : Editor
    {
        WeaponSettingSO settingSO;

        public override void OnInspectorGUI()
        {
            settingSO = target as WeaponSettingSO;

            DrawGeneral();

            if(settingSO.weaponType == WeaponType.Melee)
            {
                DrawMelee();
            }
            else if(settingSO.weaponType == WeaponType.Grenade)
            {
                DrawGrenade();
            }
            else
                DrawFirearms();

            EditorUtility.SetDirty(settingSO);
        }

        public void DrawGeneral()
        {
            GUILayout.Label("General settings", EditorStyles.boldLabel);
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Weapon name");
            settingSO.weaponName = EditorGUILayout.TextArea(settingSO.weaponName);
            GUILayout.Label("Weapon type");
            settingSO.weaponType = (WeaponType)EditorGUILayout.EnumPopup(settingSO.weaponType);
            GUILayout.Label("Weapon icon");
            settingSO.weaponIcon = (Sprite)EditorGUILayout.ObjectField(settingSO.weaponIcon, typeof(Sprite), false);
            GUILayout.Label("Shot or melee attack (swoosh) sound effect");
            settingSO.shotSFX = (AudioClip)EditorGUILayout.ObjectField(settingSO.shotSFX, typeof(AudioClip), false);
            GUILayout.Label("Recoil Vector3");
            settingSO.recoil = EditorGUILayout.Vector3Field("Recoil", settingSO.recoil);
            GUILayout.EndVertical();
        }

        public void DrawFirearms()
        {
            GUILayout.Label("Firearms settings", EditorStyles.boldLabel);
            GUILayout.BeginVertical("HelpBox");
            /// Main
            GUILayout.Label("Main settings", EditorStyles.centeredGreyMiniLabel);
            GUILayout.BeginVertical("GroupBox");
            GUILayout.Label("Weapon damage. Minimum | Maximum");
            GUILayout.BeginHorizontal();
            settingSO.damageMinimum = EditorGUILayout.IntField(settingSO.damageMinimum);
            settingSO.damageMaximum = EditorGUILayout.IntField(settingSO.damageMaximum);
            GUILayout.EndVertical();
            GUILayout.Label("Rigidbody hit force");
            settingSO.rigidBodyHitForce = EditorGUILayout.FloatField(settingSO.rigidBodyHitForce);
            GUILayout.Label("Fire rate in shot per second");
            settingSO.fireRate = EditorGUILayout.FloatField(settingSO.fireRate);
            GUILayout.Label("Spread default factor");
            settingSO.spread = EditorGUILayout.FloatField(settingSO.spread);
            GUILayout.Label("Bullet initial velocity");
            settingSO.bulletInitialVelocity = EditorGUILayout.FloatField(settingSO.bulletInitialVelocity);
            GUILayout.Label("Bullet air resiastance factor");
            settingSO.airResistanceForce = EditorGUILayout.FloatField(settingSO.airResistanceForce);
            GUILayout.Label("Bullet pool size");
            settingSO.projectilePoolSize = EditorGUILayout.IntSlider(settingSO.projectilePoolSize, 1,100);
            GUILayout.Label("Shell pool size");
            settingSO.shellsPoolSize = EditorGUILayout.IntSlider(settingSO.shellsPoolSize, 1, 100);
            GUILayout.Label("Can use scope?");
            settingSO.canUseScope = EditorGUILayout.Toggle(settingSO.canUseScope);
            if(settingSO.canUseScope)
            {
                GUILayout.Label("Scope FOV");
                settingSO.scopeFOV = EditorGUILayout.FloatField(settingSO.scopeFOV);
                GUILayout.Label("Mouse sensitivity when aim with scope X|Y");
                GUILayout.BeginHorizontal();
                settingSO.scopeSensitivityX = EditorGUILayout.FloatField(settingSO.scopeSensitivityX);
                settingSO.scopeSensitivityY = EditorGUILayout.FloatField(settingSO.scopeSensitivityY);
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            /// Effects
            GUILayout.Label("Effects", EditorStyles.centeredGreyMiniLabel);
            GUILayout.BeginVertical("GroupBox");
            GUILayout.Label("MuzzleFlash effect");
            settingSO.MuzzleFlashParticlesFX = (ParticleSystem)EditorGUILayout.ObjectField(settingSO.MuzzleFlashParticlesFX, typeof(ParticleSystem), false);
            GUILayout.Label("Reloading sound effect");
            settingSO.reloadingSFX = (AudioClip)EditorGUILayout.ObjectField(settingSO.reloadingSFX, typeof(AudioClip), false);
            GUILayout.Label("Weapon empty sound effect");
            settingSO.emptySFX = (AudioClip)EditorGUILayout.ObjectField(settingSO.emptySFX, typeof(AudioClip), false);
            GUILayout.EndVertical();
            /// Objects
            GUILayout.Label("Required objects", EditorStyles.centeredGreyMiniLabel);
            GUILayout.BeginVertical("GroupBox");
            GUILayout.Label("Shell object prefab");
            settingSO.shell = (GameObject)EditorGUILayout.ObjectField(settingSO.shell, typeof(GameObject), false);
            GUILayout.Label("Bullet prefab");
            settingSO.projectile = (GameObject)EditorGUILayout.ObjectField(settingSO.projectile, typeof(GameObject), false);
            
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }

        public void DrawGrenade()
        {
            GUILayout.Label("Grenade settings", EditorStyles.boldLabel);
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Grenade prefab");
            settingSO.grenadePrefab = (GameObject)EditorGUILayout.ObjectField(settingSO.grenadePrefab, typeof(GameObject), false);
            GUILayout.Label("Grenade throw force");
            settingSO.throwForce = EditorGUILayout.FloatField(settingSO.throwForce);
            GUILayout.EndVertical();
        }

        public void DrawMelee()
        {
            GUILayout.Label("Melee settings", EditorStyles.boldLabel);
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label("Hit sound effect");
            settingSO.meleeHitFX = (AudioClip)EditorGUILayout.ObjectField(settingSO.meleeHitFX, typeof(AudioClip), false);
            GUILayout.Label("Attack distance");
            settingSO.meleeAttackDistance = EditorGUILayout.FloatField(settingSO.meleeAttackDistance);
            GUILayout.Label("Attack rate");
            EditorGUILayout.HelpBox("Rate must be equal to attack animation duration", MessageType.Info);
            settingSO.meleeAttackRate = EditorGUILayout.FloatField(settingSO.meleeAttackRate);
            GUILayout.Label("Damage points");
            settingSO.meleeDamagePoints = EditorGUILayout.IntField(settingSO.meleeDamagePoints);
            GUILayout.Label("Rigidbody hit force");
            settingSO.rigidBodyHitForce = EditorGUILayout.FloatField(settingSO.rigidBodyHitForce);
            GUILayout.Label("Time to invoke hit in second");
            EditorGUILayout.HelpBox("Melee hit is not instant. Find hit position in your animation and bring time to this field!", MessageType.Info);
            settingSO.meleeHitTime = EditorGUILayout.FloatField(settingSO.meleeHitTime);
            GUILayout.EndVertical();
        }
    }
    
}