using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventoryButton
{
    public Button button;    
    public TMP_Text quantityText; 
    [HideInInspector] public int quantity = 0;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventoryButton> inventoryButtons;

    void Awake()
    {
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
        foreach (var inv in inventoryButtons)
        {
            inv.button.gameObject.SetActive(false);
            inv.quantityText.gameObject.SetActive(false);
        }
    }

    
    public void PickupItem(int index)
    {
        if (index < 0 || index >= inventoryButtons.Count) return;

        InventoryButton inv = inventoryButtons[index];
        inv.quantity++;
        inv.button.gameObject.SetActive(true);
        inv.quantityText.gameObject.SetActive(true);
        inv.quantityText.text = inv.quantity.ToString();
    }
}
