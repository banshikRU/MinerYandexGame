using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    public static ObstacleGenerator Instance;
    [SerializeField] private AudioClip _rockRainSFX;
    [SerializeField] private GameObject _playerParticle;
    [SerializeField] private HeartManager _heartManager;
    [SerializeField] private GameObject _stoneRain;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private GameObject _attention;
    [SerializeField] private GameObject _snake;
    [SerializeField] private GameObject _rock;
    [NonSerialized]public GameObject _curentSnake;
    private double _timeToRock;
    private double _timeToSnake;
    private double _rocksToStoneRain;
    [NonSerialized]public bool isTimerStart;
    [SerializeField] private GameObject _player;
    private List<int> level = new List<int>() {-1,0,1,2,3};
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        _curentSnake = null;
        _rocksToStoneRain = 5 + (_levelBuilder.CurentLevel * 0.03);
        _timeToRock = 5 - (_levelBuilder.CurentLevel * 0.005);
        _timeToSnake = 5 - (_levelBuilder.CurentLevel * 0.003);
        isTimerStart = true;
    }
    private void FixedUpdate()
    {
        if (isTimerStart)
        {
            _timeToRock -= Time.fixedDeltaTime;
            if (_timeToRock <= 0)
            {
                _rocksToStoneRain = 5 + (_levelBuilder.CurentLevel * 0.03);
                _timeToRock =5 - (_levelBuilder.CurentLevel * 0.005);
                GenerateRockObstacle();
            }

            _timeToSnake -= Time.fixedDeltaTime;
            if (_timeToSnake<= 0)
            {
                _timeToSnake = 7 - (_levelBuilder.CurentLevel * 0.003);
                GenerateSnakeObstacle();
            }
        }
    }
    private void GenerateRockObstacle()
    {
        if (PlayerPrefs.GetInt("FirstTraining")==0)
        {
            TrainingManager.instance.OpenSixthTraining();
            PlayerPrefs.SetInt("FirstTraining", 1);
            PlayerPrefs.Save();

        }
        int x = Random.Range(0, 100);
        if (x>90)
        {
            CameraControl.instance.ShakeCamera(2f, 10f);
            _stoneRain.SetActive(true);
            SoundManager.instance.PlaySingle(_rockRainSFX);
            StartCoroutine(GenerateRainRock());
        }
        else
        if (x > 75)
        {
            List<int> a = new List<int>(level);
            int y = Random.Range(1, 5);
            for (int i = 0; i < y; i++)
            {
                int levelX = a[Random.Range(0, a.Count)];
                a.Remove(levelX);
                Instantiate(_attention, new Vector3(levelX,_player.transform.position.y), Quaternion.identity);
                Instantiate(_rock, new Vector3(levelX, _player.transform.position.y + 15), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(_attention, _player.transform.position, Quaternion.identity);
            Instantiate(_rock, _player.transform.position+ new Vector3(0,15), Quaternion.identity);
           
        }

    }
    private void GenerateSnakeObstacle()
    {
        int x = Random.Range(0, 100);
        if (x > 75 && _curentSnake == null)
        {
            int y = Random.Range(0, 2);
            int z;
            if (y == 1)
            {
                z = 15;
            }
            else
            {
                z = -15;
            }
            _curentSnake = Instantiate(_snake, _player.transform.position + new Vector3(z,Random.Range(-5,5)), Quaternion.identity);
            if (z == -15)
            {
                _curentSnake.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    IEnumerator GenerateRainRock()
    {

        _heartManager.isRainRockActive = true;
        isTimerStart = false;
        List<int> a = new List<int>(level);
        for (int i = 0; i < Math.Ceiling(_rocksToStoneRain); i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (a.Count == 0)
            {
                a = new List<int>(level);
            }
            int levelX = a[Random.Range(0, a.Count)];
            a.Remove(levelX);
            Instantiate(_attention, new Vector3(levelX, _player.transform.position.y), Quaternion.identity);
            Instantiate(_rock, new Vector3(levelX, _player.transform.position.y + 15), Quaternion.identity);
        }
        yield return new WaitForSeconds(2f);
        isTimerStart = true;
        if (_heartManager._takeRocks != 0)
        {
            _heartManager._takeRocks = 0;
        }
        else
        {
            if (PlayerPrefs.GetInt("FirstSuperJump")==0)
            {
                TrainingManager.instance.OpenEightTraining();
                PlayerPrefs.SetInt("FirstSuperJump", 1);
                PlayerPrefs.Save();
            }
            _playerParticle.SetActive(true);
            _heartManager._takeRocks = 0;
        }
        CameraControl.instance.StopShaking();
    }
}
