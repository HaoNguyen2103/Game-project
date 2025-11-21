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

        
        LoadCharacter();
    }

    
    public void LoadCharacter()
    {
        int id = PlayerPrefs.GetInt(GlobalValue.SelectedCharacterID, 0);

        CharacterPicked = null;

        foreach (var character in Characters)
        {
            if (character.GetInstanceID() == id)
            {
                CharacterPicked = character;
                return;
            }
        }

        
        if (Characters.Length > 0)
            CharacterPicked = Characters[0];
    }
}
