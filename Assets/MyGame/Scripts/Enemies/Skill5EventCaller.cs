using UnityEngine;

public class Skill5EventCaller : MonoBehaviour
{
    public GameObject skill5HiddenObject;

    public void OnSkill5AnimationEvent()
    {
        if (skill5HiddenObject != null)
        {
            skill5HiddenObject.SetActive(true);
        }
    }
}
