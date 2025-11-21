using UnityEngine;
using System;
using System.Collections;
[AddComponentMenu("HaoNguyen/PlayerAttack")]

public class PlayerAttack : MonoBehaviour
{
    public float radius = 0.5f;
    public Transform pointAttack;
    public float attackRange = 0.5f;
    public float nextAttack = 0;
    public float timeDelay = 0.2f;
    public LayerMask enemyLayer;
    public int damegeToGive = 10;
    private Animator anim;
    private int isAttackAnimationID;
    private int isPowerAnimationID;
    private int isAttackEAnimationID;

    [Header("Special Effect Settings")]
    public GameObject specialAttackEffectPrefab; 
    public Transform effectSpawnPoint; 
    public int specialEventCharges = 0;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isAttackAnimationID = Animator.StringToHash("isAttack");
        isPowerAnimationID = Animator.StringToHash("isPower");
        isAttackEAnimationID = Animator.StringToHash("isAttackE");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger(isAttackAnimationID);
            GetkeyR();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger(isPowerAnimationID);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger(isAttackEAnimationID);
            GetkeyE();
        }
    }
    private bool GetkeyR()
    {
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + timeDelay;
            StartCoroutine(Attack());
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool GetkeyE()
    {
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + timeDelay;
            StartCoroutine(Attack());
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(pointAttack.position, attackRange, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<IcanTakeDamage>()?.TakeDamage(damegeToGive, pointAttack.position, gameObject);
        }
        yield return null;
    }
    public void OnAttackEvent()
    {
        if (specialEventCharges > 0 && specialAttackEffectPrefab != null && effectSpawnPoint != null)
        {
            specialEventCharges--;
            
            GameObject effect = Instantiate(specialAttackEffectPrefab, effectSpawnPoint.position, Quaternion.identity);
            Destroy(effect, 1f); 
        }
    }
    public void AddSpecialEventCharges(int amount)
    {
        specialEventCharges += amount;
    }
    private void OnDrawGizmos()
    {
        if (pointAttack == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointAttack.position, attackRange);
    }
}