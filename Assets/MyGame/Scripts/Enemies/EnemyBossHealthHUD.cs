using UnityEngine;
using   UnityEngine.UI;
using System.Collections;
public class EnemyBossHealthHUD : MonoBehaviour
{
    [Header("UI Components")]
    public Image enemyBossIcon;
    [SerializeField] private Slider healthBar;
    public float showDuration = 5f;

    private Coroutine hideRoutine;

    void Awake()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = 1f;
            healthBar.value = 1f;
            healthBar.interactable = false;
            gameObject.SetActive(false);
        }
    }
    public void SetHealth(float value)
    {
        if (healthBar != null)
            healthBar.value = Mathf.Clamp01(value);
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

    public void SetEnemyBossIcon(Sprite icon)
    {
        if (enemyBossIcon != null && icon != null)
            enemyBossIcon.sprite = icon;
    }
}

