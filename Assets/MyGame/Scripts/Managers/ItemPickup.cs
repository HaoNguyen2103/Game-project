using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int buttonIndex; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
       
            InventoryManager.Instance.PickupItem(buttonIndex);

            
            Destroy(gameObject);
        }
    }
}
