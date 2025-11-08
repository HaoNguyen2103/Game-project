using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    public GameObject optionPanel;
    public void OnClickOption()
    {
        optionPanel.SetActive(true);
    }
    public void OnClickOptionExit()
    {
        optionPanel.SetActive(false);
    }
}
