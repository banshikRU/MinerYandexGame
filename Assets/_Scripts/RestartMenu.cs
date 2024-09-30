using System;
using TMPro;
using UnityEngine;

public class RestartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoresCount;
    [SerializeField] private TextMeshProUGUI _moneyCount;
    [SerializeField] private GameObject _plusCoinsButton;
    public static RestartMenu instance;
    private void OnEnable()
    {
        instance = this;
        RestartMenuInitialize();
    }
    public void RestartMenuInitialize()
    {
        _scoresCount.text = ScoreManager.instance.curentScores.ToString();
        _moneyCount.text = RunTimeCoinManager.instance.AbsoluteCoinsCount.ToString();
    }
    public void PlusRunTimeCoins(int value)
    {
        _plusCoinsButton.SetActive(false);
        YandexManager.ysdk.ShowRewardedAdvPlusCoinsRunTime(value);
    }
}
