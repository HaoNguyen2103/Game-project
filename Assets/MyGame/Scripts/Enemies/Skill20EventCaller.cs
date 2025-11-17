using UnityEngine;
using System.Collections;
public class Skill20EventCaller : MonoBehaviour
{
   
    public GameObject skill5Object;
    public GameObject skill20Object;
    public float activeDuration = 10f;

    public void OnSkill5AnimationEvent()
    {
        if (skill5Object != null)
            StartCoroutine(ActivateThenDisable(skill5Object, activeDuration));
    }
    public void OnSkill20AnimationEvent()
    {
        if (skill20Object != null)
            StartCoroutine(ActivateThenDisable(skill20Object, activeDuration));
    }
    private IEnumerator ActivateThenDisable(GameObject obj, float duration)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}