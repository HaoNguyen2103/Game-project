using UnityEngine;

public class CharacterKey : MonoBehaviour
{
    public GameObject IPCharacter; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            if (IPCharacter != null)
            {
                IPCharacter.SetActive(!IPCharacter.activeSelf); 
            }
        }
    }
    public void ToggleCharacterPanel()
    {
        IPCharacter.SetActive(!IPCharacter.activeSelf);
    }
}
