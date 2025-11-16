using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    [Range(0, 100)]
    public float dropChance;
}
public class Enemy : MonoBehaviour, IcanTakeDamage
{
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    public EnemyHealthHUD hudPrefab;
    public Sprite enemyIcon;
    public float attackRange = 1.5f;
    public int damagePlayer = 2;
    public float attackRate = 1f;

    [HideInInspector] public bool IsDead = false;

    private float nextAttackTime = 0f;
    private float currentHealth;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyAI enemyAI;
    private Transform playerTarget;

    private EnemyHealthHUD hudInstance;
    private static EnemyHealthHUD currentActiveHUD;

    [Header("Drop Item")]
    public DropItem[] dropItems;
    public int maxDropCount = 3;
    public Transform dropPoint;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();


        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTarget = playerObj.transform;


        if (hudPrefab != null)
        {
            Canvas mainCanvas = Object.FindFirstObjectByType<Canvas>();
            if (mainCanvas != null)
            {
                hudInstance = Instantiate(hudPrefab, mainCanvas.transform);
                hudInstance.SetEnemyIcon(enemyIcon);
                hudInstance.UpdateHealth(currentHealth, maxHealth);
                hudInstance.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (IsDead) return;


        if (playerTarget != null && Vector2.Distance(transform.position, playerTarget.position) < 5f)
        {
            Vector3 scale = transform.localScale;
            scale.x = (playerTarget.position.x < transform.position.x)
                ? -Mathf.Abs(scale.x)
                : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsDead) return;
        if (Time.time < nextAttackTime) return;

        Player player = collision.GetComponent<Player>();
        if (player != null && !player.GetIsDead())
        {

            Vector2 directionToPlayer = player.transform.position - transform.position;
            bool isFacingPlayer = (transform.localScale.x > 0 && directionToPlayer.x > 0) ||
                                  (transform.localScale.x < 0 && directionToPlayer.x < 0);

            if (!isFacingPlayer) return;
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
            {
                if (anim != null)
                    anim.SetTrigger("isAttacking");

                player.TakeDamage(damagePlayer, transform.position, gameObject);

                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    public void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitSource)
    {
        if (IsDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (hudInstance != null)
        {
            if (currentActiveHUD != null && currentActiveHUD != hudInstance)
                currentActiveHUD.HideImmediate();

            hudInstance.SetEnemyIcon(enemyIcon);
            hudInstance.UpdateHealth(currentHealth, maxHealth);
            currentActiveHUD = hudInstance;
        }

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (IsDead) return;
        IsDead = true;

        if (anim != null)
            anim.SetTrigger("isDead");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (enemyAI != null)
            enemyAI.enabled = false;

        if (hudInstance != null)
            Destroy(hudInstance.gameObject, 0.1f);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }
        for (int i = 0; i < maxDropCount; i++)
        {
            GameObject itemToDrop = GetRandomDrop();
            if (itemToDrop != null)
            {
                Vector3 spawnPos = dropPoint.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                Instantiate(itemToDrop, spawnPos, Quaternion.identity);
            }
        }
        Destroy(gameObject, 2f);
    }
    private GameObject GetRandomDrop()
    {
        float roll = Random.Range(0f, 100f);
        float cumulative = 0f;

        foreach (DropItem dropItem in dropItems)
        {
            cumulative += dropItem.dropChance;
            if (roll <= cumulative)
            {
                return dropItem.itemPrefab;
            }
        }

        return null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
