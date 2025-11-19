using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPoint : MonoBehaviour
{
    public int characterID;

    public void OnSelect()
    {
        CharacterHolder.Instance.SaveCharacter(characterID);
        SceneManager.LoadScene("LV1"); 
    }
}
