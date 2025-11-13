using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour, IcanTakeDamage
{
    public float maxHealth = 1000f;
    public float currentHealth;
    public UnityEvent<float> onHealthPercentChanged;
    public UnityEvent onBossDeath;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        onHealthPercentChanged?.Invoke(100f);
    }

    public void TakeDamage(int damage, Vector2 hitPoint, GameObject hitSource)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        float hpPercent = (currentHealth / maxHealth) * 100f;
        onHealthPercentChanged?.Invoke(hpPercent);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        onBossDeath?.Invoke();
    }
}
