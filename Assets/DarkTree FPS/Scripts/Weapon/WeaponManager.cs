/// DarkTreeDevelopment (2019) DarkTree FPS v1.2
/// If you have any questions feel free to write me at email --- darktreedevelopment@gmail.com ---
/// Thanks for purchasing my asset!

using UnityEngine;
using System.Collections.Generic;

namespace DarkTreeFPS
{
    public class WeaponManager : MonoBehaviour
    {
        //A public list which get all aviliable weapons on Start() and operate with them
        public List<Weapon> weapons;

        public bool UseNonPhysicalReticle = true;

        public bool haveMeleeWeaponByDefault = true;
        public Weapon melleeDefaultWeapon;
        public Weapon grenade;

        public List<Slot> slots;
        [Range(1, 9)]
        
        private int slotsSize = 4;

        public int switchSlotIndex = 0;
        public int currentWeaponIndex;
        public Slot activeSlot;
        //public Weapon primarySlot;
        //public Weapon secondarySlot;

        [Tooltip("Scope image used for riffle aiming state")]
        public GameObject scopeImage;
        [Tooltip("Crosshair image")]
        public GameObject reticleDynamic;
        public GameObject reticleStatic;

        [Tooltip("Animator that contain pickup animation")]
        public Animator weaponHolderAnimator;

        [HideInInspector]
        public GameObject tempGameobject;

        //Transform where weapons will dropped on Drop()
        private Transform playerTransform;

        private Transform swayTransform;

        private Inventory inventory;
        
        [HideInInspector]
        //public Weapon currentWeapon;


        private void Start()
        {
            for (int i = 0; i < slotsSize; i++)
            {
                Slot slot_temp = gameObject.AddComponent<Slot>();

                slots.Add(slot_temp);
            }

            if (haveMeleeWeaponByDefault)
            {
                slots[0].storedWeapon = melleeDefaultWeapon;
                activeSlot = slots[0];
            }

            if(grenade != null)
            {
                slots[3].storedWeapon = grenade;
            }

            swayTransform = FindObjectOfType<Sway>().GetComponent<Transform>();

            scopeImage.SetActive(false);

            if (UseNonPhysicalReticle)
            {
                reticleStatic.SetActive(true);
                reticleDynamic.SetActive(false);
            }
            else
            {
                reticleStatic.SetActive(false);
                reticleDynamic.SetActive(true);
            }

            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            foreach (Weapon weapon in swayTransform.GetComponentsInChildren<Weapon>(true))
            {
                weapons.Add(weapon);
            }

            inventory = FindObjectOfType<Inventory>();
        }

        private void Update()
        {
            //Check if we are trying to switch weapons we have
            SlotInput();

            if (Input.GetKeyDown(KeyCode.G))
            {
                DropWeapon();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                DropAllWeapons();
            }
        }

        public Slot FindFreeSlot()
        {
            foreach (Slot slot in slots)
            {
                if (slot.IsFree())
                    return slot;
            }

            return null;
        }
        
        private void SlotInput()
        {
            if (Input.GetButtonDown("Slot0")) { switchSlotIndex = 0; SlotChange(); }
            else if (Input.GetButtonDown("Slot1")) { switchSlotIndex = 1; SlotChange(); }
            else if (Input.GetButtonDown("Slot2")) { switchSlotIndex = 2; SlotChange(); }
            else if (Input.GetButtonDown("Slot3")) { switchSlotIndex = 3; SlotChange(); }
            else if (Input.GetButtonDown("Slot4")) { switchSlotIndex = 4; SlotChange(); }
            else if (Input.GetButtonDown("Slot5")) { switchSlotIndex = 5; SlotChange(); }
            else if (Input.GetButtonDown("Slot6")) { switchSlotIndex = 6; SlotChange(); }
            else if (Input.GetButtonDown("Slot7")) { switchSlotIndex = 7; SlotChange(); }
            else if (Input.GetButtonDown("Slot8")) { switchSlotIndex = 8; SlotChange(); }
        }

        public void ShowGrenade()
        {
            //print("Show grenade");
            HideAll();
            //activeSlot = slots[3];
            grenade.gameObject.SetActive(true);
        }

        private void SlotChange()
        {
            print("Slot change");
            if (grenade.gameObject.activeInHierarchy)
                grenade.gameObject.SetActive(false);

            print("Slot change 2");
            if (activeSlot != null && activeSlot.storedWeapon != null)
            {///////  012            /// 012     
                if (slots.Count > switchSlotIndex)
                {
                    if (slots[switchSlotIndex].storedWeapon != null)
                    {
                        activeSlot.storedWeapon.gameObject.SetActive(false);

                        activeSlot = null;
                        activeSlot = slots[switchSlotIndex];
                        activeSlot.storedWeapon.gameObject.SetActive(true);

                        print("I equip slot number" + switchSlotIndex);

                        weaponHolderAnimator.Play("Unhide");
                    }
                }
                else
                    return;
            }
            else
                return;
        }

        //EquipWeapon is called from Item class on pickup. Item class passes arguments to EquipWeapon
        public bool IsWeaponAlreadyPicked(string weaponName)
        {
            foreach (Slot slot in slots)
            {
                if (slot.storedWeapon != null && slot.storedWeapon.weaponName == weaponName)
                    return true;
            }
            return false;
        }

        public Weapon FindWeaponByName(string name)
        {
            foreach (Slot slot in slots)
            {
                if (slot.storedWeapon.weaponName == name)
                    return slot.storedWeapon;
            }

            return null;
        }

        public void EquipWeapon(string weaponName, GameObject temp)
        {
            print(temp.name);

            grenade.gameObject.SetActive(false);

            if (!IsWeaponAlreadyPicked(weaponName))
            {
                if (FindFreeSlot() != null)
                {
                    if (activeSlot != null)
                        activeSlot.storedWeapon.gameObject.SetActive(false);

                    activeSlot = FindFreeSlot();

                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.weaponName == weaponName)
                        {
                            activeSlot.storedWeapon = weapon;
                            print(activeSlot.storedWeapon.currentAmmo);
                            print(temp.GetComponent<WeaponPickup>().ammoInWeaponCount);
                            activeSlot.storedWeapon.currentAmmo = temp.GetComponent<WeaponPickup>().ammoInWeaponCount;
                            activeSlot.storedDropObject = temp;
                            activeSlot.storedDropObject.SetActive(false);
                            temp = null;
                            activeSlot.storedWeapon.gameObject.SetActive(true);

                            switchSlotIndex = switchSlotIndex + 1;

                            weaponHolderAnimator.Play("Unhide");

                            break;
                        }
                    }

                }
            }
            else
            {
                Weapon weapon_temp = FindWeaponByName(weaponName);
                
                //temp.SetActive(false);
                //temp = null;
                print("Weapon already exist");
            }
        }

        private Slot FindEquipedSlot()
        {
            foreach (Slot slot in slots)
            {
                if (!slot.IsFree())
                    return slot;
            }

            return null;
        }

        public void HideWeaponOnDeath()
        {
            weaponHolderAnimator.SetLayerWeight(1, 0);
            weaponHolderAnimator.SetBool("HideWeapon", true);
        }

        public void UnhideWeaponOnRespawn()
        {
            if (UseNonPhysicalReticle)
            {
                reticleStatic.SetActive(true);
                reticleDynamic.SetActive(false);
            }
            else
            {
                reticleStatic.SetActive(false);
                reticleDynamic.SetActive(true);
            }

            weaponHolderAnimator.SetLayerWeight(1, 1);
            weaponHolderAnimator.SetBool("HideWeapon", false);
        }
        
        public void HideAll()
        {
            print("Hide weapon works");

            if (activeSlot != null)
            {
                    activeSlot.storedWeapon.gameObject.SetActive(false);
                    
                    weaponHolderAnimator.Play("HideWeapon");
            }
        }

        public void UnhideWeaponAfterGrenadeDrop()
        {
            grenade.gameObject.SetActive(false);

            if(activeSlot != null)
            {
                weaponHolderAnimator.Play("Unhide");
                activeSlot.storedWeapon.gameObject.SetActive(true);
            }
        }

        public void DropAllWeapons()
        {
            weaponHolderAnimator.SetLayerWeight(1, 0);
            weaponHolderAnimator.SetBool("HideWeapon", true);

            foreach (Slot slot in slots)
            {
                if (!slot.IsFree())
                {
                    if (slot.storedWeapon.weaponType != WeaponType.Melee && haveMeleeWeaponByDefault)
                    {
                        if (slot.storedWeapon == activeSlot.storedWeapon)
                        {
                            DropWeapon();
                        }
                        else
                        {
                            slot.storedWeapon.gameObject.SetActive(false);
                            if (slot.storedDropObject)
                            {
                                slot.storedDropObject.SetActive(true);
                                slot.storedDropObject.transform.position = playerTransform.transform.position + playerTransform.forward * 0.5f;
                                slot.storedDropObject = null;
                                slot.storedWeapon = null;
                            }
                        }
                    }
                }
            }

            if (haveMeleeWeaponByDefault)
            {
                activeSlot = slots[0];
                activeSlot.storedWeapon.gameObject.SetActive(true);
            }
        }

        public void ChangeWeaponMobile(bool equipMelee)
        {
            print("I work!");

            if (!equipMelee)
            {
                print("I equip no melee");

                if (slots[1].storedWeapon != null && slots[2].storedWeapon != null)
                {
                    if (activeSlot.storedWeapon == slots[2].storedWeapon)
                    {
                        HideAll();
                        switchSlotIndex = 1;
                        SlotChange();
                    }
                    else if (activeSlot.storedWeapon == slots[1].storedWeapon)
                    {
                        HideAll();
                        switchSlotIndex = 2;
                        SlotChange();
                    }
                    else if (activeSlot.storedWeapon == slots[0].storedWeapon)

                    {
                        if (slots[1].storedWeapon != null)
                        {
                            HideAll();
                            switchSlotIndex = 1;
                            SlotChange();
                        }
                        else if (slots[2].storedWeapon != null)
                        {
                            HideAll();
                            switchSlotIndex = 2;
                            SlotChange();
                        }
                    }
                }
                else if (activeSlot.storedWeapon == slots[0].storedWeapon)
                {
                    if (slots[1].storedWeapon != null)
                    {
                        HideAll();
                        switchSlotIndex = 1;
                        SlotChange();
                    }
                    else if(slots[2].storedWeapon != null)
                    {
                        HideAll();
                        switchSlotIndex = 2;
                        SlotChange();
                    }
                }
                
            }
            else if(equipMelee && haveMeleeWeaponByDefault)
            {
                print("I equip melee");
                
                HideAll();
                switchSlotIndex = 0;
                SlotChange();
            }
        }

        private void DropWeapon()
        {
            if (activeSlot != null)
            {
                if (activeSlot != slots[0] && haveMeleeWeaponByDefault)
                {
                    activeSlot.storedDropObject.GetComponent<WeaponPickup>().ammoInWeaponCount = activeSlot.storedWeapon.currentAmmo;
                    activeSlot.storedWeapon.gameObject.SetActive(false);
                    activeSlot.storedDropObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
                    activeSlot.storedDropObject.SetActive(true);
                    activeSlot.storedDropObject = null;
                    activeSlot.storedWeapon = null;
                    activeSlot = FindEquipedSlot();

                    if (activeSlot != null)
                        activeSlot.storedWeapon.gameObject.SetActive(true);

                    weaponHolderAnimator.Play("Unhide");
                }
            }
        }
        
        public void DropWeaponFromSlot(int slot)
        {
            if(activeSlot.storedWeapon == slots[slot].storedWeapon)
            {
                DropWeapon();
            }
            else
            {
                slots[slot].storedDropObject.GetComponent<WeaponPickup>().ammoInWeaponCount = slots[slot].storedWeapon.currentAmmo;
                slots[slot].storedDropObject.transform.position = playerTransform.position + playerTransform.forward * 0.5f;
                slots[slot].storedDropObject.SetActive(true);
                slots[slot].storedDropObject = null;
                slots[slot].storedWeapon = null;
            }
        }
    }
}
