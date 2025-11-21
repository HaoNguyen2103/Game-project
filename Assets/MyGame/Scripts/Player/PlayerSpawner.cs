using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public CinemachineCamera virtualCamera;

    void Start()
    {
        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        if (CharacterHolder.Instance == null || CharacterHolder.Instance.CharacterPicked == null)
        {
            return;
        }

        
        GameObject player = Instantiate(CharacterHolder.Instance.CharacterPicked, spawnPoint.position, Quaternion.identity);

        
        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
        }
        UpdateHealth ui = Object.FindFirstObjectByType<UpdateHealth>();
        if (ui != null)
        {
            Player playerComponent = player.GetComponent<Player>();
            if (playerComponent != null)
            {
                ui.SetPlayer(playerComponent);
            }
        }
    }
}
