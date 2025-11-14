using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [Header("Boss Settings")]
    public EnemyBossHealthHUD hudPrefab;
    public Sprite enemyBossIcon;
    public int damagePlayer = 5;
    public float attackRate = 1f;

    private Rigidbody2D rb;
    private EnemyBossAI enemyBossAI;
    private Animator anim;
    private EnemyBossHealthHUD hudInstance;
    private static EnemyBossHealthHUD currentActiveHUD;

    private float nextAttackTime = 0f;
    private BossHealth bossHealth;
    private bool isDead = false;
    private bool hasFlippedToPlayer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyBossAI = GetComponent<EnemyBossAI>();
        anim = GetComponentInChildren<Animator>();
        bossHealth = GetComponent<BossHealth>();
    }

    void Start()
    {
        if (hudPrefab != null)
        {
            Canvas mainCanvas = Object.FindFirstObjectByType<Canvas>();
            if (mainCanvas != null)
            {
                hudInstance = Instantiate(hudPrefab, Object.FindFirstObjectByType<Canvas>().transform);
                hudInstance.SetEnemyBossIcon(enemyBossIcon);
                hudInstance.SetHealth(1f);
                hudInstance.gameObject.SetActive(false);
            }
        }

        if (bossHealth != null)
        {
            bossHealth.onHealthPercentChanged.AddListener(UpdateHUD);
            bossHealth.onBossDeath.AddListener(Die);
        }
    }

    private void UpdateHUD(float hpPercent)
    {
        if (hudInstance == null) return;

        if (currentActiveHUD != null && currentActiveHUD != hudInstance)
            currentActiveHUD.HideImmediate();

        hudInstance.SetEnemyBossIcon(enemyBossIcon);
        hudInstance.SetHealth(hpPercent / 100f);
        hudInstance.Show();
        currentActiveHUD = hudInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (Time.time < nextAttackTime) return;

        Player player = collision.GetComponent<Player>();
        if (player != null && !player.GetIsDead())
        {




            Vector3 scale = transform.localScale;
            scale.x = (player.transform.position.x < transform.position.x)
                ? -Mathf.Abs(scale.x)
                : Mathf.Abs(scale.x);
            transform.localScale = scale;
            hasFlippedToPlayer = true;


            if (anim != null)
                anim.SetTrigger("isAttacking");

            player.TakeDamage(damagePlayer, transform.position, gameObject);

            nextAttackTime = Time.time + attackRate;
        }
    }


    public void FinishAttack()
    {

        if (hasFlippedToPlayer)
        {

            hasFlippedToPlayer = false;
        }


    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null)
            anim.SetTrigger("isDead");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (enemyBossAI != null) enemyBossAI.enabled = false;

        if (hudInstance != null)
            Destroy(hudInstance.gameObject, 0.1f);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        Destroy(gameObject, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, col.bounds.extents.x);
        }
    }
}
