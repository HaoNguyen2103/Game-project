using UnityEngine;
[AddComponentMenu("HaoNguyen/Player")]

public class Player : MonoBehaviour, IcanTakeDamage
{
    public int Maxhealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private Animator anim;
    private int isDeadID;
    private PlayerController playerController;
    private PlayerAttack playerAttack;
    private PlayerFire playerFire;
    //private float timeDelay = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        currentHealth = Maxhealth;
        anim = GetComponentInChildren<Animator>();
        isDeadID = Animator.StringToHash("isDead");
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerFire = GetComponent<PlayerFire>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitDirection)
    {
        if (isDead) return;
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            DeadPlayer();
        }
    }
    private void DeadPlayer()
    {
        anim.SetTrigger("isDead");
        playerController.enabled = false;
        playerAttack.enabled = false;
        playerFire.enabled = false;
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}
