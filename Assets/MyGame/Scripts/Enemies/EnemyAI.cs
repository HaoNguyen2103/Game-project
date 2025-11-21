using UnityEngine;
using System.Collections;

[AddComponentMenu("HaoNguyen/EnemyAI")]
public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform PointA;
    public Transform PointB;

    [Header("Movement Settings")]
    public float speed = 2f;
    public float minDistance = 0.1f;
    public float idleTime = 1f;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;

    private int isWalkId;
    private bool isWaiting = false;
    public bool isAttacking = false;
    private bool isWalking = false;

    private Enemy enemyScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyScript = GetComponent<Enemy>();

        isWalkId = Animator.StringToHash("isWalking");

        target = PointA;
        StartWalking();
    }

    void Update()
    {

        if (!isWaiting && !isAttacking && !enemyScript.IsDead)
            Patrol();
    }

    private void Patrol()
    {
        Vector2 direction = (target.position - transform.position).normalized;


        if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            StartCoroutine(WaitAndTurn());
            return;
        }

        rb.linearVelocity = direction * speed;


        if ((direction.x > 0 && transform.localScale.x < 0) ||
            (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        anim.SetTrigger(isWalkId);
    }

    private IEnumerator WaitAndTurn()
    {
        isWaiting = true;
        StopWalking();
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(idleTime);

        Flip();
        target = (target == PointA) ? PointB : PointA;

        StartWalking();
        isWaiting = false;
    }

    public void StartWalking()
    {
        if (!isWalking)
        {
            anim.SetTrigger(isWalkId);
            isWalking = true;
        }
    }

    public void StopWalking()
    {
        if (isWalking)
        {
            anim.ResetTrigger(isWalkId);
            rb.linearVelocity = Vector2.zero;
            isWalking = false;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
