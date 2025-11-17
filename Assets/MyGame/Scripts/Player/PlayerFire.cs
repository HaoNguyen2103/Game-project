using UnityEngine;
[AddComponentMenu("HaoNguyen/PlayerFire")]
public class PlayerFire : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletsPosition;
    public float bulletSpeed = 20f;
    public float energyCost = 10f;

    private PlayerController playerController;
    private Player player;
    private float timeDestroy = 2f;
    public AudioClip shootClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        { 
            if (player != null && player.UseEnergy(energyCost))
                
            {
                Shoot();
                AudioManager.Instance.PlayEnemysfxmusic(shootClip);
            }
        }
    }
    private void Shoot()
    {
        GameObject bullets = Instantiate(bulletPrefab, bulletsPosition.position, Quaternion.identity);
        Rigidbody2D rb = bullets.GetComponent<Rigidbody2D>();
        float dir = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(dir * bulletSpeed, 0f);
        float angle = (dir > 0) ? 0f : 180f;
        bullets.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
