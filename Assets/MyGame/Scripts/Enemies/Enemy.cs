using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour, IcanTakeDamage
{
    [Header("Enemy Settings")]
    public float maxHealth = 100f;
    public Slider healthBar;
    public float currentHealth;
    private bool isDead = false;
    private Rigidbody2D rb;
    //private float timeDestroy = 0.2f;
    public float nexAttackTime = 0f;
    public float attackRate = 1f;
    public int damagePlayer = 5;
    private EnemyAI enemyAI;
    private Animator anim;
    public void TakeDamage(int dameAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead) return;
        currentHealth -= dameAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

    }
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        //Animator anim = GetComponentInChildren<Animator>();
        //if (anim != null)
        //{
        anim.SetTrigger("isDead");
        //}
        // EnemyAI enemyAI = GetComponent<EnemyAI>();
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }
        Destroy(gameObject, 2f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
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
