using UnityEngine;
using UnityEngine.UI;
public class UpdateHealth : MonoBehaviour
{
    public Image healthBar;
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
    }
}
