using TMPro;
using UnityEngine;
using System;

public class RunTimeCoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinsCountText;
    private int _absoluteCoinsCount;
    private double _coinsCountToText;
    public static RunTimeCoinManager instance;
    public int AbsoluteCoinsCount { get => _absoluteCoinsCount; set => _absoluteCoinsCount = value; }

    private void Awake()
    {
        instance = this;
    }
    public void SaveCoinsCount()
    {
        AbsoluteCoinsCount +=(int)Math.Floor(ScoreManager.instance.curentScores / 100f);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + AbsoluteCoinsCount);
        PlayerPrefs.Save();
        YandexManager.ysdk.SaveToLeaderBoard(ScoreManager.instance.curentScores);
        //PlayerProgress.instance.playerInfo.Coins += AbsoluteCoinsCount;
        //Debug.Log(AbsoluteCoinsCount + "Abs");
        //Debug.Log(PlayerProgress.instance.playerInfo.Coins);
        //PlayerProgress.instance.Save();
    }
    public void UpdateCoinsCount()
    {
        UpdateCoinsCountText();
    }
    public void PlusCoins(int count)
    {
        AbsoluteCoinsCount += count;
        UpdateCoinsCountText();
    }
    private void UpdateCoinsCountText()
    {
        _coinsCountToText = AbsoluteCoinsCount +Math.Floor(ScoreManager.instance.curentScores / 100f);
        _coinsCountText.text = _coinsCountToText.ToString();
    }
}
