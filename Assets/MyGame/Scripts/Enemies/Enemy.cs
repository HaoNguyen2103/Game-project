using UnityEngine;

public class Enemy : MonoBehaviour, IcanTakeDamage
{
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    private bool isDead = false;
    private Rigidbody2D rb;
    //private float timeDestroy = 0.2f;
    public float nexAttackTime;
    public float attackRate = 1f;
    public int damagePlayer = 20;
    private EnemyAI enemyAI;
    public void TakeDamage(int dameAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead) return;
        currentHealth -= dameAmount;
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }
    private void Die()
    {
        rb.linearVelocity = Vector2.zero;
        enemyAI.enabled = false;
        // Handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null && !player.GetIsDead())
        {
            player.TakeDamage(20, transform.position, gameObject);
            {
                nexAttackTime = Time.time + attackRate;
                IcanTakeDamage damageable = collision.GetComponent<IcanTakeDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damagePlayer, transform.position, gameObject);
                }
            }
        }
    }
}
