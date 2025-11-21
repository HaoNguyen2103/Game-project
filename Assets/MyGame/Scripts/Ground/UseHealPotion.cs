using UnityEngine;

public class UseHealPotion : MonoBehaviour
{
    private Player player;      
    public int healAmount = 50;
    void Start()
    {
        player = Object.FindFirstObjectByType<Player>();

    }

    public void OnClickHealPotion()
    {
        
        if (player != null)
        {
            player.Heal(healAmount);
        }
    }
}
