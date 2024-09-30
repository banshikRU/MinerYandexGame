using System;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public static Canvas Instance;
    [SerializeField] private GameObject _blackOutBackGround;
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject _playMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _restartMenu;
    [SerializeField] private GameObject _controlButtons;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _market;
    [SerializeField] private GameObject _adsBlock;
    [SerializeField] private GameObject _startTrainingBlock;
    [SerializeField] private GameObject _playTrainingBlock;
    [SerializeField] private GameObject _lastTrainingBlock;
    [SerializeField] private GameObject _rateGameButton;
    [NonSerialized] public int isFirstTraining;
    private void Awake()
    {
        Instance = this;
    }
    #region StartMenu
    public void OpenStartMenu()
    {
        if (PlayerPrefs.GetInt("RateGame") == 0) { _rateGameButton.SetActive(true); }
        if (isFirstTraining == 1) { OpenStartTrainingMenu(); } 
        _restartMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        _startMenu.SetActive(true);
    }
    public void CloseStartMenu()
    {
        _startMenu.SetActive(false);
    } 
    #endregion
    #region PlayMenu
    public void OpenPlayMenu()
    {
        if (isFirstTraining ==1){ CloseStartTrainingMenu();OpenPlayTrainingMenu(); }
        CloseSettingMenu();
        _startMenu.SetActive(false);
        _playMenu.SetActive(true);
    }
    public void ClosePlayMenu()
    {
        _playMenu.SetActive(false);
    } 
    #endregion
    #region RestartMenu
    public void OpenRestartMenu()
    {
        _playMenu.SetActive(false);
        _restartMenu.SetActive(true);
    }
    public void CloseRestartMenu()
    {
        _restartMenu.SetActive(false);
    } 
    #endregion
    #region PauseMenu
    public void OpenPauseMenu()
    {
        _playMenu.SetActive(false);
        _pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        _playMenu.SetActive(true) ;
        _pauseMenu.SetActive(false);
    } 
    #endregion
    #region SettingMenu
    public void OpenSettingMenu()
    {
        _blackOutBackGround.SetActive(true);
        _settingMenu.SetActive(true);
    }
    public void CloseSettingMenu()
    {
        _blackOutBackGround.SetActive(false);
        _settingMenu.SetActive(false);
    }
    #endregion
    #region MarketMenu
    public void OpenMarketMenu()
    {
        _blackOutBackGround.SetActive(true);
        _market.SetActive(true);
    }
    public void CloseMarketMenu()
    {
        _blackOutBackGround.SetActive(false);
        _market.SetActive(false);
    }
    #endregion
    #region AdsBlock
    public void OpenAdsMenu()
    {
        _adsBlock.SetActive(true);
    }
    public void CloseAdsMenu()
    {
        _adsBlock.SetActive(false);
    }
    #endregion
    #region FirstTrainingStartBlock
    public void OpenStartTrainingMenu()
    {
        _startTrainingBlock.SetActive(true);
    }
    public void CloseStartTrainingMenu()
    {
        _startTrainingBlock.SetActive(false);
    }
    #endregion
    #region FirstTrainingPlayBlock
    public void OpenPlayTrainingMenu()
    {
        _playTrainingBlock.SetActive(true);
    }
    public void ClosePlayTrainingMenu()
    {
        _playTrainingBlock.SetActive(false);
    }
    #endregion
    #region LastTrainingPlayBlock
    public void OpenLastTrainingMenu()
    {
        _lastTrainingBlock.SetActive(true);
    }
    public void CloseLastTrainingMenu()
    {
        _lastTrainingBlock.SetActive(false);
    }
    #endregion
}
