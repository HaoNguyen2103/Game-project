using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHealth : MonoBehaviour
{
    public Image healthBar;
    public Image energyBar;
    public Image xpBar;
    public TMP_Text levelText;
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        healthBar.fillAmount = (float)player.GetCurrentHealth() / player.GetMaxHealth();
        energyBar.fillAmount = (float)player.GetCurrentEnergy() / player.GetMaxEnergy();
        xpBar.fillAmount = player.currentXP / player.xpToLevelUp;
        levelText.text = " " +player.level;
    }
}
