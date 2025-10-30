using UnityEngine;
[AddComponentMenu("HaoNguyen/TimeDestroyObject")]
public class TimeDestroyObject : MonoBehaviour
{
    private float destroyTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
