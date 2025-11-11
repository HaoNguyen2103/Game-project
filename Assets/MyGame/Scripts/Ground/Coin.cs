using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public AudioClip coinClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin(1);
            AudioManager.Instance.PlaySfx(coinClip);
            Destroy(gameObject);
        }
        
    }
}
