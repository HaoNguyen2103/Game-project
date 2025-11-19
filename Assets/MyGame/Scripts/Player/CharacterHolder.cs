using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    public static CharacterHolder Instance;

    public GameObject[] Characters; 
    public GameObject CharacterPicked;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadCharacter();
    }

    public void SaveCharacter(int id)
    {
        PlayerPrefs.SetInt(GlobalValue.SelectedCharacterID, id);
        PlayerPrefs.Save();
    }

    public void LoadCharacter()
    {
        int id = PlayerPrefs.GetInt(GlobalValue.SelectedCharacterID, 0);
        CharacterPicked = Characters[id];
    }
}
