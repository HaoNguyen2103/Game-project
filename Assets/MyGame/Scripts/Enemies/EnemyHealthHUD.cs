using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthHUD : MonoBehaviour
{
    [Header("UI Components")]
    public Image enemyIcon;
    public Slider healthBar;
    public float showDuration = 5f;

    private Coroutine hideRoutine;

    void Awake()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = 1f;
            healthBar.value = 1f;
            gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthBar != null)
            healthBar.value = Mathf.Clamp01(currentHealth / maxHealth);

        Canvas.ForceUpdateCanvases(); 

        if (!gameObject.activeSelf)
            Show();

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
    }

    public void HideImmediate()
    {
        if (hideRoutine != null)
            StopCoroutine(hideRoutine);
        gameObject.SetActive(false);
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showDuration);
        gameObject.SetActive(false);
    }

    public void SetEnemyIcon(Sprite icon)
    {
        if (enemyIcon != null && icon != null)
            enemyIcon.sprite = icon;
    }
}
