using UnityEngine;

public class SwitchObjectsPermanently : MonoBehaviour
{
    public GameObject objectA;
    public GameObject[] objectsToActivate;

    private bool switched = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (switched) return;

        if (other.CompareTag("Player"))
        {
            if (objectA != null)
                objectA.SetActive(false);

            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            switched = true;
        }
    }
}
