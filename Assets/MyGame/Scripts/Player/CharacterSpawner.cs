using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        Instantiate(CharacterHolder.Instance.CharacterPicked,
                    spawnPoint.position,
                    Quaternion.identity);
    }
}
