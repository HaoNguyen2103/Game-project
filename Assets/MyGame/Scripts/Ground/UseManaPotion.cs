using UnityEngine;

public class UseManaPotion : MonoBehaviour
{
    public float recoverAmount = 30f;
    private Player player;

    void Start()
    {
        player = Object.FindFirstObjectByType<Player>();

    }

    public void OnClickManaPotion()
    {
        if (player != null)
        {
            player.RecoverEnergy(recoverAmount);

        }
    }
}
