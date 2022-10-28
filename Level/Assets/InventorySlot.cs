using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    Item item;


    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Instantiate(item.model, new Vector3(gameManager.instance.playerScript.itemDropPoint.transform.position.x, 
            gameManager.instance.playerScript.itemDropPoint.transform.position.y, 
            gameManager.instance.playerScript.itemDropPoint.transform.position.z), gameManager.instance.playerScript.itemDropPoint.transform.rotation);

        Inventory.instance.Remove(item);

    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
