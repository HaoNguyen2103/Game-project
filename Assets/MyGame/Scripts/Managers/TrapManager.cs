using Unity.VisualScripting;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public int damage = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IcanTakeDamage playerTakeDamage = collision.GetComponent<IcanTakeDamage>();
            if (playerTakeDamage != null)
            {
                Debug.Log("Player va vao bay, gay sat thuong: " + damage);
                playerTakeDamage.TakeDamage(damage, Vector2.zero, gameObject);

            }
        }
    }
}
