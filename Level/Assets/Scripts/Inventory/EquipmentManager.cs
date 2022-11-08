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

    public Equipment[] currentEquipment;
    public Consumable[] consumables;
    [SerializeField] Gun starterGun;
    [SerializeField] Sword starterSword;


    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[equipSlots];
        Equip(starterGun);
        Equip(starterSword);
    }

    public void Equip (Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.equipmentSlot;

        Equipment oldEquipment = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            Inventory.instance.Add(oldEquipment);
        }


        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        currentEquipment[slotIndex] = newEquipment;

        if(newEquipment is Weapon)
        {
            gameManager.instance.playerScript.weaponModel.GetComponent<MeshRenderer>().sharedMaterial = newEquipment.model.GetComponent<MeshRenderer>().sharedMaterial;
            gameManager.instance.playerScript.weaponModel.GetComponent<MeshFilter>().sharedMesh = newEquipment.model.GetComponent<MeshFilter>().sharedMesh;

            if (newEquipment.GetType() == typeof(Gun))
                gameManager.instance.playerScript.gunStats = (Gun)newEquipment;
            else if (newEquipment.GetType() == typeof(Sword))
                gameManager.instance.playerScript.swordStat = (Sword)newEquipment;
        }



    }
    
    public void Unequip(int slotIndex)
    {
        Equipment oldEquipment = null;
        if (currentEquipment[slotIndex] != null)
        {
            TutorialManager.instance.unequipButton = true;

            if (currentEquipment[slotIndex] is Weapon)
            {
                if (currentEquipment[slotIndex].GetType() == typeof(Gun))
                {
                    gameManager.instance.playerScript.gunStats = null;
                }
                else if (currentEquipment[slotIndex].GetType() == typeof(Sword))
                    gameManager.instance.playerScript.swordStat = null;
                gameManager.instance.playerScript.weaponModel.GetComponent<MeshRenderer>().sharedMaterial = null;
                gameManager.instance.playerScript.weaponModel.GetComponent<MeshFilter>().sharedMesh = null;
            }
            
            oldEquipment = currentEquipment[slotIndex];
            Inventory.instance.Add(oldEquipment);

            currentEquipment[slotIndex] = null;


            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
        else
        {
            TutorialManager.instance.unequipButton = false;
        }
    }

    public void unEquipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
            Unequip(i);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            unEquipAll();
    }
}
