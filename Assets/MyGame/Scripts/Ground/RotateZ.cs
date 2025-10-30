using UnityEngine;
[AddComponentMenu("HaoNguyen/RotateZ")]
public class RotateZ : MonoBehaviour
{
    public float speed = 45f;
    public bool useLocalSpace = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float delta = speed * Time.deltaTime;
        if (useLocalSpace)
        {
            transform.Rotate(0f, 0f, delta, Space.Self);
        }
        else
        {
            transform.Rotate(0f, 0f, delta, Space.World);
        }

    }
}
