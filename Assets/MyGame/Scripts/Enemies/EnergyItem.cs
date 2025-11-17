using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    public float energyRestore = 20f; 
    public bool destroyOnPickup = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            
            player.RecoverEnergy(energyRestore);

            
            if (destroyOnPickup)
                Destroy(gameObject);
        }
    }
}
