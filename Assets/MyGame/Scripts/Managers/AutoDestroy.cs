using UnityEngine;
[AddComponentMenu("NguyenHao/AutoDestroy")]
public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
