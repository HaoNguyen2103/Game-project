using Unity.VisualScripting;
using UnityEngine;
[AddComponentMenu("HaoNguyen/Player")]

public class Player : MonoBehaviour, IcanTakeDamage
{
    public int Maxhealth = 300;
    private int currentHealth;

    public float maxEnergy = 100f;
    private float currentEnergy;
    public float energyRegenRate = 1f;
    private bool isDead = false;
    private Animator anim;
    private int isDeadID;
    private PlayerController playerController;
    private PlayerAttack playerAttack;
    private PlayerFire playerFire;

    public int level = 1;
    public float currentXP = 0;
    public float xpToLevelUp = 100f;
    
    void Start()

    {
        currentHealth = Maxhealth;
        currentEnergy = maxEnergy;
     
        anim = GetComponentInChildren<Animator>();
        isDeadID = Animator.StringToHash("isDead");
        playerController = GetComponent<PlayerController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerFire = GetComponent<PlayerFire>();
    }

    // Update is called once per frame
    void Update()
    {
        RegenEnergy();
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
        AudioManager.Instance.PlayPlayerDeath();
    }
    public bool GetIsDead()
    {
        return isDead;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return Maxhealth;
    }
    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }
    public float GetMaxEnergy()
    {
        return maxEnergy;
    }
    public bool UseEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void RegenEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += energyRegenRate * Time.deltaTime;
            if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
        }
    }
    public void RecoverEnergy(float amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
    }
    public void GainXP(float amount)
    {
        currentXP += amount;

        while (currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        currentXP -= xpToLevelUp;
        level++;
        xpToLevelUp *= 1.2f;
    }
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > Maxhealth)
            currentHealth = Maxhealth;
    }
}