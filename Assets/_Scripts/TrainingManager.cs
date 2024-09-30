using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _firstTraining;
    [SerializeField] private GameObject _secondTraining;
    [SerializeField] private GameObject _thirdTraining;
    [SerializeField] private GameObject _fouthTraining;
    [SerializeField] private GameObject _fifthTraining;
    [SerializeField] private GameObject _sixthTraining;
    [SerializeField] private GameObject _seventhTraining;
    [SerializeField] private GameObject _eightTraining;
    [SerializeField] private GameObject _firstTrainingPlayMenu;
    [SerializeField] private GameObject _lastTraining;
    [SerializeField] private GameObject _lastfirstTraining;
    public static TrainingManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void OpenSecondTraining()
    {
        _firstTrainingPlayMenu.SetActive(true);
        _firstTraining.SetActive(false);
        _secondTraining.SetActive(true);
    }
    public void OpenThirdTraining()
    {
        _secondTraining.SetActive(false);
        _thirdTraining.SetActive(true);
    }
    public void OpenFourthTraining()
    {
        _thirdTraining.SetActive(false);
        _fouthTraining.SetActive(true);
    }
    public void OpenFifthTraining()
    {
        _fouthTraining.SetActive(false);
        _fifthTraining.SetActive(true);
    }
    public void UnPauseGame()
    {
        _firstTrainingPlayMenu.SetActive(false);
        _fifthTraining.SetActive(false);
        _seventhTraining.SetActive(false);
        _eightTraining.SetActive(false);
        _gameManager.ResumeTraining();
    }
    public void PauseGame()
    {
        _firstTrainingPlayMenu.SetActive(true);
        _gameManager.PauseTrainingGame();
    }
    public void OpenSixthTraining()
    {
        PauseGame();
        _sixthTraining.SetActive(true);
    }
    public void OpenSeventhTraining()
    {
        _sixthTraining.SetActive(false);
        _seventhTraining.SetActive(true);
    }
    public void OpenEightTraining()
    {
        PauseGame();
        _eightTraining.SetActive(true);
    }
    public void OpenLastTraining()
    {
        _lastfirstTraining.SetActive(false);
        _lastTraining.SetActive(true);
    }
    public void CloseTraining()
    {
        _lastTraining.SetActive(false);
        Canvas.Instance.CloseLastTrainingMenu();
    }
}
