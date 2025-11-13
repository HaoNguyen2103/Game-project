using UnityEngine;

public class BossSummoner : MonoBehaviour
{
    [Header("Minion Spawn Points (Set in Editor)")]
    public GameObject[] minions;

    public void SummonMinions()
    {
        foreach (GameObject minion in minions)
        {
            if (minion != null && !minion.activeInHierarchy)
            {
                minion.SetActive(true); 
            }
        }
    }
}
