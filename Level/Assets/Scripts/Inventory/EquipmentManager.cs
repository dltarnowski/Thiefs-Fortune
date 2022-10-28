using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Weapon[] currentWeapon;


    public delegate void OnWeaponChanged(Weapon newWeapon, Weapon oldWeapon);
    public OnWeaponChanged onWeaponChanged;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(ItemSlot)).Length;
        currentWeapon = new Weapon[numSlots];
    }

    public void Equip (Weapon newWeapon)
    {
        
        int slotIndex = (int)newWeapon.itemSlot;

        Weapon oldWeapon= null;

        if(currentWeapon[slotIndex] != null)
        {
            oldWeapon = currentWeapon[slotIndex];
            Inventory.instance.Add(oldWeapon, slotIndex);
        }

        if(onWeaponChanged != null)
        {
            onWeaponChanged.Invoke(newWeapon, oldWeapon);
        }

        currentWeapon[slotIndex] = newWeapon;
        gameManager.instance.playerScript.weaponModel.GetComponent<MeshRenderer>().sharedMaterial = newWeapon.model.GetComponent<MeshRenderer>().sharedMaterial;
        gameManager.instance.playerScript.weaponModel.GetComponent<MeshFilter>().sharedMesh = newWeapon.model.GetComponent<MeshFilter>().sharedMesh;

        gameManager.instance.playerScript.gunStats = null;
        gameManager.instance.playerScript.swordStat = null;

        if (newWeapon.GetType() == typeof(Gun))
            gameManager.instance.playerScript.gunStats = (Gun)newWeapon;
        else if (newWeapon.GetType() == typeof(Sword))
            gameManager.instance.playerScript.swordStat = (Sword)newWeapon;

    }
    
    public void Unequip(int slotIndex)
    {
        if(currentWeapon[slotIndex] != null)
        {
            if(currentWeapon[slotIndex] != null)
            {
                if (currentWeapon[slotIndex].GetType() == typeof(Gun))
                {
                    gameManager.instance.playerScript.gunStats = null;
                }
                else if (currentWeapon[slotIndex].GetType() == typeof(Sword))
                    gameManager.instance.playerScript.swordStat = null;
                gameManager.instance.playerScript.weaponModel.GetComponent<MeshRenderer>().sharedMaterial = null;
                gameManager.instance.playerScript.weaponModel.GetComponent<MeshFilter>().sharedMesh = null;
            }
            Weapon oldWeapon = currentWeapon[slotIndex];
            Inventory.instance.Add(oldWeapon, (int)oldWeapon.itemSlot);

            currentWeapon[slotIndex] = null;

            if (onWeaponChanged != null)
            {
                onWeaponChanged.Invoke(null, oldWeapon);
            }
        }
    }

    public void unEquipAll()
    {
        for (int i = 0; i < currentWeapon.Length; i++)
            Unequip(i);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            unEquipAll();
    }
}
