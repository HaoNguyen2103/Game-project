using UnityEngine;

public class swordItem : MonoBehaviour
{
    public int charges = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack playerAttack = collision.GetComponentInChildren<PlayerAttack>();
        if (playerAttack != null)
        {
            playerAttack.AddSpecialEventCharges(charges);
            Destroy(gameObject);
        }
    }

}
