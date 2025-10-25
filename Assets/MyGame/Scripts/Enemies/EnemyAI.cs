using UnityEngine;
using System.Collections;

[AddComponentMenu("HaoNguyen/EnemyAI")]
public class EnemyAI : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float speed = 2f;
    public float minDistance = 0.1f;
    public float idleTime = 1f;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isIdle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        target = PointA;
    }

    void Update()
    {
        if (!isIdle)
            Patrol();
    }

    private void Patrol()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        
        if (direction.x != 0)
            sprite.flipX = direction.x < 0;

        
        anim.SetBool("isWalking", true);
        rb.linearVelocity = direction * speed;

        
        if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            StartCoroutine(IdleThenTurn());
        }
    }

    private IEnumerator IdleThenTurn()
    {
        isIdle = true;

        
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isWalking", false); 

        yield return new WaitForSeconds(idleTime);

        
        target = target == PointA ? PointB : PointA;

        isIdle = false;
    }

    private void OnDrawGizmos()
    {
        if (PointA != null && PointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(PointA.position, PointB.position);
        }
    }
}
