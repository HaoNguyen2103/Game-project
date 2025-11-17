using UnityEngine;

public class BossSummoner : MonoBehaviour
{
    [Header("Minion Spawn Points (Set in Editor)")]
    public GameObject[] minions;

    public void SummonMinions(int ammount)
    {
        int count = 0;
        foreach (GameObject minion in minions)
        {
            if (!minion.activeInHierarchy)
            {
                minion.SetActive(true);
                count++;

                if (count >= ammount)
                    break;
            }
        }
    }
}
