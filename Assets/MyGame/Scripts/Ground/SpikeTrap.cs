using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 100;
    [Header("Audio Settings")]
    public AudioClip spikeSound;
    public GameObject fxPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IcanTakeDamage cantakeDamage = collision.GetComponent<IcanTakeDamage>();
            if (cantakeDamage != null)
            {
                IcanTakeDamage takeDamage = collision.GetComponent<IcanTakeDamage>();
                takeDamage.TakeDamage(damageAmount, transform.position, gameObject);
                if (spikeSound != null)
                {
                    AudioManager.Instance.PlaySfx(spikeSound);
                }
                if (fxPrefab != null)
                {
                    Instantiate(fxPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
