using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float speed = 10f;       
    public float lifetime = 5f;     
    public int damage = 20;        
    public GameObject explodeEffect; 

    private void Start()
    {
       
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        
        transform.position += -transform.right * speed * Time.deltaTime;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<IcanTakeDamage>();
            if (player != null)
                player.TakeDamage(damage, Vector2.zero, null);
        }

        if (explodeEffect != null)
            Instantiate(explodeEffect, transform.position, Quaternion.identity);

        Destroy(gameObject); 
    }
}
