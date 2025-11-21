using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public Image healthBar;
    public Image energyBar;
    public Image xpBar;
    public TMP_Text levelText;

    private void Awake()
    {
    
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   
    public void UpdateHealth(float current, float max)
    {
        if (healthBar != null)
            healthBar.fillAmount = Mathf.Clamp01(current / max);
    }

   
    public void UpdateEnergy(float current, float max)
    {
        if (energyBar != null)
            energyBar.fillAmount = Mathf.Clamp01(current / max);
    }

  
    public void UpdateXP(float current, float max)
    {
        if (xpBar != null)
            xpBar.fillAmount = Mathf.Clamp01(current / max);
    }

    
    public void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = "Level " + level;
    }
}
