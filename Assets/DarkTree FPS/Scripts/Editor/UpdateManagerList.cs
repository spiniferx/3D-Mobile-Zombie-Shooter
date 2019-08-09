using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DarkTreeFPS
{
    [CustomEditor(typeof(WeaponManager))]
    public class UpdateManagerList : Editor
    {
        WeaponManager manager;
        Sway swayTransform;

        private void OnEnable()
        {
            manager = FindObjectOfType<WeaponManager>();
            swayTransform = FindObjectOfType<Sway>();
        }

        public override void OnInspectorGUI()
        {
            Editor editor = Editor.CreateEditor(manager);
            editor.DrawDefaultInspector();

            if (GUILayout.Button("Update weapons"))
            {
                manager.weapons.Clear();
                manager.weapons = GetAllWeapons();
            }
        }

        List<Weapon> GetAllWeapons()
        {
            List<Weapon> weaponsInScene = new List<Weapon>();

            foreach (Weapon weapon in swayTransform.GetComponentsInChildren<Weapon>(true))
            {
                    weaponsInScene.Add(weapon);
            }
            return weaponsInScene;
        }
    }
}