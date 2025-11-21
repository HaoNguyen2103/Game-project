using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public GameObject characterPrefab;  

    public void OnSelect()
    {
        
        int id = characterPrefab.GetInstanceID();
        CharacterHolder.Instance.SaveCharacter(id);

        SceneManager.LoadScene("LV1");
    }
}
