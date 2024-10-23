using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
public class PlayerScore : UnityEvent<int> { }
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private SmothMovement _playerMover;
    [SerializeField] private BuffManager _buffManager;
    [SerializeField] private TextMeshProUGUI _scores;
    [SerializeField] private TextMeshProUGUI _damageMulti;
    [SerializeField] private GameObject _fire;
    [SerializeField] private Animator scoreAnimation;
    [SerializeField] private float _timeToMultiply;
    [SerializeField] private float timeToFire;
    public int curentScores;
    private string _curentMultipleDamageText;
    public static PlayerScore _scoreEvent;
    private float t;
    private float _timeMyulti;
    private int scoresToMulti;
    private bool isFire;
    private void Awake()
    {
        Language.lanSwitch.AddListener(SwitchLanguage);
    }
    private void OnEnable()
    {
        Language.Instance.LanguageSwitch();
    }
    private void Start()
    {
        instance = this;
        isFire = false;
        t = 0;
        curentScores = 0;
        _timeMyulti = _timeToMultiply;
        _scoreEvent = new PlayerScore();
        UpdateScoresMulti(1f);
        _scoreEvent.AddListener(ScoreUpdate);
    }
    private void SwitchLanguage(string lang)
    {
        if (lang == "ru")
        {   _curentMultipleDamageText = "Урон*X";
        }
        else
        {
            _curentMultipleDamageText = "Damage*X";
        }
    }
    private void UpdateScoresMulti (float scoresMulti)
    {
        _damageMulti.text = _curentMultipleDamageText + scoresMulti;
    } 
    public void ScoreUpdate(int scores)
    {
        if (scores >=10)
        {
            scoresToMulti += scores;
            if (isFire == true)
            {
                t = timeToFire;

            }
            else
            {
                t = timeToFire;
                isFire = true;
            }
        }

        curentScores += scores ;
        _scores.text = curentScores.ToString();
        scoreAnimation.Play("ScoreAnimation");
        RunTimeCoinManager.instance.UpdateCoinsCount();
    }
    private void FixedUpdate()
    {
        if (isFire)
        {
            _timeMyulti -= Time.fixedDeltaTime;
            t -= Time.fixedDeltaTime;
            if (_timeMyulti <= 0)
            {
                _timeMyulti = _timeToMultiply;
                if (scoresToMulti >=190)
                {
                    _playerMover._damageMultiplier = 2.2f;
                }
                else if (scoresToMulti >= 140)
                {
                    _playerMover._damageMultiplier = 1.9f;
                }
                else if (scoresToMulti >= 90)
                {
                    _playerMover._damageMultiplier = 1.8f;
                }
                else if (scoresToMulti >= 70)
                {
                    _playerMover._damageMultiplier = 1.7f;
                }
                else if (scoresToMulti >= 30)
                {
                    _playerMover._damageMultiplier = 1.6f;
                }
                else
                {
                    _playerMover._damageMultiplier = 1;
                }
                UpdateScoresMulti(_playerMover._damageMultiplier);
                scoresToMulti = 0;
            }
            if (t <= 0)
            {
                _playerMover._damageMultiplier = 1;
                UpdateScoresMulti(_playerMover._damageMultiplier);
                //_fire.SetActive(false);
                isFire = false;
                t = timeToFire;
                _timeMyulti = _timeToMultiply;
            }
        }
    }
}
