using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
public class Enemy : MonoBehaviour, IcanTakeDamage
{
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    public EnemyHealthHUD hudPrefab;
    public float currentHealth;
    private bool isDead = false;
    private Rigidbody2D rb;
    //private float timeDestroy = 0.2f;
    public float nexAttackTime = 0f;
    public float attackRate = 1f;
    public int damagePlayer = 5;
    private EnemyAI enemyAI;
    private Animator anim;
    public Sprite enemyIcon;
    private EnemyHealthHUD hudInstance;
    private static EnemyHealthHUD currentActiveHUD;
    public void TakeDamage(int dameAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead) return;
        currentHealth -= dameAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (hudInstance != null)
        {
            if (currentActiveHUD != null && currentActiveHUD != hudInstance)
            {
                currentActiveHUD.HideImmediate();
            }


            hudInstance.SetEnemyIcon(enemyIcon);
            hudInstance.UpdateHealth(currentHealth, maxHealth);
            
            currentActiveHUD = hudInstance;

        }

        if (currentHealth <= 0)
            Die();

    }
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        
        
        anim.SetTrigger("isDead");
       
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }
        if (hudInstance != null)
            Destroy(hudInstance.gameObject, 0.1f);

        Destroy(gameObject, 2f);
        
    }
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponentInChildren<Animator>();
        if (hudPrefab != null)
        {
            Canvas mainCanvas = Object.FindFirstObjectByType<Canvas>();
            if (mainCanvas != null)
            {
                hudInstance = Instantiate(hudPrefab, Object.FindFirstObjectByType<Canvas>().transform);
                hudInstance.SetEnemyIcon(enemyIcon);
                if (hudInstance.healthBar != null)
                    hudInstance.healthBar.value = currentHealth / maxHealth;
                hudInstance.gameObject.SetActive(false);
            }
        }
    }

    
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (Time.time < nexAttackTime) return;
        Player player = collision.GetComponent<Player>();
        if (player != null && !player.GetIsDead())
        {

            Vector3 scale = transform.localScale;
            scale.x = (player.transform.position.x < transform.position.x) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;


            if (anim != null)
                anim.SetTrigger("isAttacking");


            player.TakeDamage(damagePlayer, transform.position, gameObject);


            nexAttackTime = Time.time + attackRate;
        }
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
