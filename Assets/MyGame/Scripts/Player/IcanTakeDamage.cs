using UnityEngine;

public interface IcanTakeDamage
{
    void TakeDamage(int damageAmount, Vector2 hitPoint, GameObject hitDirection);
}
