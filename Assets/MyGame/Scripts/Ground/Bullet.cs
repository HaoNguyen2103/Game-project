using UnityEngine;
[AddComponentMenu("HaoNguyen/Bullet")]
public class Bullet : MonoBehaviour
{
    public GameObject fxPrefab;
    private float destroyTime = 3f;
    public int damavalue = 10;
    private bool hasHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IcanTakeDamage damage = collision.GetComponent<IcanTakeDamage>();

            if (damage != null)
            {
                hasHit = true;
                damage.TakeDamage(damavalue, Vector2.zero, gameObject);
                Destroy(gameObject);
            }
            Instantiate(fxPrefab, transform.position, Quaternion.identity);
            

        }

    }
}
