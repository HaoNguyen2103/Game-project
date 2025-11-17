using UnityEngine;

public class FirreBoss : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;

    
    public void ShootBullet()
    {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}