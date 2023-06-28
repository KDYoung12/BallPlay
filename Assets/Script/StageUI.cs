using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textStage;
    [SerializeField]
    private TextMeshProUGUI textCoinCount;

    public void UpdateTextStage(string stageName)
    {
        textStage.text = stageName;
    }

    public void UpdateCoinCount(int current, int max)
    {
        textCoinCount.text = $"Coin {current}/{max}";
    }
}
