using System.Collections;
using TMPro;
using UnityEngine;

public class TextCoinUpdate : MonoBehaviour
{
    public TextMeshProUGUI textCoin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdateCoin());
    }

    // Update is called once per frame
    IEnumerator UpdateCoin()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.2f); // Update every 0.5 second
            textCoin.text = GameManager.Instance.GetCoin().ToString();
        }
    }
}
