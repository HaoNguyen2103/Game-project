using UnityEngine;
using UnityEngine.UI;
public class UpdateHealth : MonoBehaviour
{
    public Image healthBar;
    public Image energyBar;
    public Image xpBar;
    public Text levelText;
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.GetCurrentHealth() / player.GetMaxHealth();
        energyBar.fillAmount = (float)player.GetCurrentEnergy() / player.GetMaxEnergy();
        xpBar.fillAmount = player.currentXP / player.xpToLevelUp;
        levelText.text = "Lv " + player.level;
    }
}
