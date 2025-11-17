using UnityEngine;
using System.Collections;

public class BossPhaseController : MonoBehaviour
{
    private BossHealth bossHealth;
    private BossSummoner summoner;
    private Animator anim;

    private bool phase80Triggered = false;
    private bool phase60Triggered = false;
    private bool phase40Triggered = false;
    private bool phase20Triggered = false;
    private bool phase5Triggered = false;

    void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
        summoner = GetComponent<BossSummoner>();
        anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        if (bossHealth != null)
            bossHealth.onHealthPercentChanged.AddListener(OnHealthChanged);
    }

    void OnDisable()
    {
        if (bossHealth != null)
            bossHealth.onHealthPercentChanged.RemoveListener(OnHealthChanged);
    }

    private void OnHealthChanged(float hpPercent)
    {
        if (!phase80Triggered && hpPercent <= 80f && hpPercent > 60f)
        {
            phase80Triggered = true;
            TriggerBossSkill("Skill_80", 1.5f,5);
        }
        else if (!phase60Triggered && hpPercent <= 60f && hpPercent > 40f)
        {
            phase60Triggered = true;
            TriggerBossSkill("Skill_60");
        }
        else if (!phase40Triggered && hpPercent <= 40f && hpPercent > 20f)
        {
            phase40Triggered = true;
            TriggerBossSkill("Skill_40", 1.5f,11);
        }
        else if (!phase20Triggered && hpPercent <= 20f && hpPercent > 5f)
        {
            phase20Triggered = true;
            TriggerBossSkill("Skill_20");
        }
        else if (!phase5Triggered && hpPercent <= 5f)
        {
            phase5Triggered = true;
            TriggerBossSkill("Skill_5");
        }
    }

    private void TriggerBossSkill(string skillTrigger, float summonDelay, int summonAmount)
    {
        if (anim != null)
            anim.SetTrigger(skillTrigger);

        if (summoner != null)
            StartCoroutine(DelayedSummon(delay: summonDelay, summonAmount: summonAmount));
    }
    private void TriggerBossSkill(string skillTrigger)
    {
        if (anim != null)
            anim.SetTrigger(skillTrigger);
    }

    private IEnumerator DelayedSummon(float delay, int summonAmount)
    {
        yield return new WaitForSeconds(delay);
        summoner.SummonMinions(summonAmount);
    }
}
