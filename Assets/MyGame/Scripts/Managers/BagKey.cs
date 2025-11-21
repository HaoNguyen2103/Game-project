using UnityEngine;

public class BagKey : MonoBehaviour
{
    public GameObject BagOpen; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            if (BagOpen != null)
            {
                BagOpen.SetActive(!BagOpen.activeSelf); 
            }
        }
    }
    public void ToggleInventory()
    {

        BagOpen.SetActive(!BagOpen.activeSelf);
    }
}
