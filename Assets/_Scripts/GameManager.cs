using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SmothMovement _smoth;
    [SerializeField] private ScoreManager _scoreManager;
    public bool _isPlay;
    public void Initialize()
    {
      //  AdsControl();
        CheckForFirstTraining();
        Time.timeScale = 0f;
        _isPlay = false;
        Canvas.Instance.OpenStartMenu();
        ObstacleGenerator.Instance.isTimerStart = false;
    }
    public void StartGame()
    {
       // YandexManager.ysdk.GameReadyApiStart();
        PlayCountIterator.instance.CounterUpdater();
        if (PlayerPrefs.GetInt("FirstTraining") == 1)
        {
            _isPlay = true;
            Time.timeScale = 1f;
            _smoth.StartSmoothMoveDown();
            Canvas.Instance.OpenPlayMenu();
            ObstacleGenerator.Instance.isTimerStart = true;
        }
        else
        {
            Canvas.Instance.OpenPlayMenu();
        }
    }
    public void ResumeTraining()
    {
        _isPlay = true;
        Time.timeScale = 1f;
        _smoth.StartSmoothMoveDown();
        ObstacleGenerator.Instance.isTimerStart = true;
    }
    public void PauseTrainingGame()
    {

        ObstacleGenerator.Instance.isTimerStart = false;
        _isPlay = false;
        Time.timeScale = 0f;
        _smoth.CancelInvoke();
    }
    public void PlayerDeath()
    {
        YandexManager.ysdk.GameReadyApiStop();
        RunTimeCoinManager.instance.SaveCoinsCount();
        Time.timeScale = 0f;
        Canvas.Instance.OpenRestartMenu();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        YandexManager.ysdk.GameReadyApiStop();
        ObstacleGenerator.Instance.isTimerStart = false;
        Canvas.Instance.OpenPauseMenu();
        _isPlay = false;
        _smoth.CancelInvoke();
        Time.timeScale = 0f;
    }
    public void AdsControl()
    {
        if (PlayCountIterator.instance._counter % 2 == 0 || PlayCountIterator.instance._counter == 0)
        {
            YandexManager.ysdk.ShowFullScreenAdv();
        }
        if ((PlayCountIterator.instance._counter % 2 == 0 || PlayCountIterator.instance._counter == 0) && PlayerPrefs.GetInt("FirstTraining") ==1)
        {
            Canvas.Instance.OpenAdsMenu();
        }
        else
        {
            Canvas.Instance.CloseAdsMenu();
        }
    }
    private void CheckForFirstTraining()
    {
        if (PlayerPrefs.GetInt("FirstTraining") == 0)
        {
            Canvas.Instance.isFirstTraining = 1;
        }
        else if (PlayerPrefs.GetInt("FirstTraining") == 1)
        {
            Canvas.Instance.isFirstTraining = 0;
        }
        if (PlayerPrefs.GetInt("LastTraining") == 0 && (PlayerPrefs.GetInt("FirstTraining") == 1))
        {
            Canvas.Instance.OpenLastTrainingMenu();
            PlayerPrefs.SetInt("LastTraining", 1);
            PlayerPrefs.Save();
        }
    }
}
