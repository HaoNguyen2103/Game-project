using System.Collections;
using Unity.Mathematics;
using UnityEngine;
[AddComponentMenu("HaoNguyen/PlayerController")]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public LayerMask groundLayer;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private Rigidbody2D rb;
    private bool isJumping = false;

    private bool facingRight = true;
    private Animator anim;

    private int isWalkID;
    private int isJumpID;
    public float dropVelocity = -5f;
    public float dropDuration = 0.2f;
    private Collider2D playerCol;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        isWalkID = Animator.StringToHash("isWalk");
        isJumpID = Animator.StringToHash("isJump");
        playerCol = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        if (isJumping && IsGrounded())
        {
            isJumping = false;
            anim.ResetTrigger(isJumpID);
        }
        else if (!IsGrounded() && !isJumping)
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DropDown());
        }
    }
    IEnumerator DropDown()
    {

        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        int count = playerCol.Overlap(filter, results);

        for (int i = 0; i < count; i++)
        {
            if (results[i] != null && results[i].GetComponent<PlatformEffector2D>() != null)
            {
                Physics2D.IgnoreCollision(playerCol, results[i], true);
            }
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, dropVelocity);

        yield return new WaitForSeconds(dropDuration);

        for (int i = 0; i < count; i++)
        {
            if (results[i] != null && results[i].GetComponent<PlatformEffector2D>() != null)
            {
                Physics2D.IgnoreCollision(playerCol, results[i], false);
            }
        }
    }
    void Jump()
    {
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (IsGrounded() && math.abs(rb.linearVelocity.x) > 0.1f)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    public bool FaceRight()
    {
        return facingRight;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
