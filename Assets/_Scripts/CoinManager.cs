using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinsCountText;
    private int _coinsCount;
    public static CoinManager instance;

    public int CoinsCount { get => _coinsCount; set => _coinsCount = value; }

    private void Awake()
    {
       instance = this;
    }
    public void Initialize()
    {
        LoadCoinsCount();
        UpdateCoinsText();
    }
    private void LoadCoinsCount()
    {
        // CoinsCount = PlayerProgress.instance.playerInfo.Coins;
        CoinsCount = PlayerPrefs.GetInt("Coins");
    }
    private void UpdateCoinsText()
    {
        _coinsCountText.text = CoinsCount.ToString();
    }
    public void AddCoins(int value)
    {
        CoinsCount += value;
        //PlayerProgress.instance.playerInfo.Coins = CoinsCount;
        //PlayerProgress.instance.Save();
        PlayerPrefs.SetInt("Coins", CoinsCount);
        PlayerPrefs.Save();
        UpdateCoinsText();
    }
    public void SubstructCoins(int value)
    {
        CoinsCount-= value;
        //PlayerProgress.instance.playerInfo.Coins = CoinsCount;
        //PlayerProgress.instance.Save();
        PlayerPrefs.SetInt("Coins", CoinsCount);
        PlayerPrefs.Save();
        UpdateCoinsText();
    }
}
