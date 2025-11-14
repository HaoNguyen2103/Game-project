using UnityEngine;
using System.Collections;

[AddComponentMenu("HaoNguyen/EnemyBossAI")]
public class EnemyBossAI : MonoBehaviour
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

    private int isIdleId;
    private int isWalkId;

    private bool isWaiting = false;
    public bool isAttacking = false;
    private bool isWalking = false;

    private bool playerTouching = false;
    private Transform player;

    private float savedFacingX;   


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        isIdleId = Animator.StringToHash("isIdle");
        isWalkId = Animator.StringToHash("isWalking");

        target = PointA;
        StartWalking();
    }

    void Update()
    {
        if (isWaiting || isAttacking)
            return;

        if (!playerTouching)
        {
            Patrol();
            FaceDirectionOfMovement();
        }
        else
        {
            
            rb.linearVelocity = Vector2.zero;
            FacePlayer();
        }
    }

    private void Patrol()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            StartCoroutine(WaitAndSwitchPoint());
            return;
        }

        rb.linearVelocity = direction * speed;
        anim.SetTrigger(isWalkId);
    }

    private IEnumerator WaitAndSwitchPoint()
    {
        isWaiting = true;
        StopWalking();
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger(isIdleId);

        yield return new WaitForSeconds(idleTime);

        target = (target == PointA) ? PointB : PointA;

        StartWalking();
        isWaiting = false;
    }


    private void FaceDirectionOfMovement()
    {
        float vx = rb.linearVelocity.x;

        if (vx > 0.05f)
            SetFacing(1);
        else if (vx < -0.05f)
            SetFacing(-1);
    }

    private void FacePlayer()
    {
        if (player == null) return;

        float dir = player.position.x - transform.position.x;
        SetFacing(dir >= 0 ? 1 : -1);
    }

    private void SetFacing(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerTouching = true;
            player = col.transform;

            
            savedFacingX = transform.localScale.x;

            StopWalking();  
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerTouching = false;
            player = null;

            
            Vector3 scale = transform.localScale;
            scale.x = savedFacingX;
            transform.localScale = scale;

            StartWalking();
        }
    }

    public void StartWalking()
    {
        if (!isWalking)
        {
            anim.ResetTrigger(isIdleId);
            anim.SetTrigger(isWalkId);
            isWalking = true;
        }
    }

    public void StopWalking()
    {
        if (isWalking)
        {
            anim.ResetTrigger(isWalkId);
            anim.SetTrigger(isIdleId);
            rb.linearVelocity = Vector2.zero;
            isWalking = false;
        }
    }
}
