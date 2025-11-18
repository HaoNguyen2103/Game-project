using UnityEngine;

public class HpItem : MonoBehaviour
{
    public int healAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject); 
        }
    }
}
