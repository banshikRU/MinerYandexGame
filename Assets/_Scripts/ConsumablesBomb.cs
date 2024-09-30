using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class ConsumablesBomb : MonoBehaviour
{
    public static ConsumablesBomb instance;
    private int _bombsCount;
    [SerializeField] private int _levelBombCanDestroyed;
    [SerializeField] private AudioClip _explosionSfx;
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bombs;
    [SerializeField] private SmothMovement _movement;
    [SerializeField] private TextMeshProUGUI _bombsCountText;
    [SerializeField] private GameObject _bombsRunTime;
    [SerializeField] private TextMeshProUGUI _bombsCountTextRunTime;
    private void Awake()
    {
        instance = this;
    }
    public int Bombs { get => _bombsCount; set => _bombsCount = value; }
    public void Initialize()
    {
        if (UpdateBombsCount() > 0)
        {
            _bombs.SetActive(true);
            UpdateBombsCountText();
        }
        else
        {
            _bombs.SetActive(false);
        }
    }
    private void OnEnable()
    {
        InitializeRunTimeBombs();
    }
    public void InitializeRunTimeBombs()
    {
        if (UpdateBombsCount()>0)
        {
            _bombsRunTime.SetActive(true);
            UpdateBombsCountTextRunTime();
        }
        else
        {
            _bombsRunTime.SetActive(false);
        }
    }
    public int UpdateBombsCount()
    {
        Bombs = PlayerPrefs.GetInt("Bombs");
        return Bombs;
    }
    private void UpdateBombsCountText()
    {
        _bombsCountText.text = _bombsCount.ToString();
    }
    private void UpdateBombsCountTextRunTime()
    {
        _bombsCountTextRunTime.text = _bombsCount.ToString();
    }
    public void BombExplosion()
    {
          List<RaycastHit2D> blocks;
          blocks = Physics2D.RaycastAll(_player.transform.position, new Vector2(0, -1), _levelBombCanDestroyed, _floorLayer).ToList();
          foreach (RaycastHit2D block in blocks)
          {
              Debug.Log(block);
              block.transform.gameObject.GetComponent<IAmBlock>().ExplosiveDestroy();
          }
        _bombsCount--;
        PlayerPrefs.SetInt("Bombs",PlayerPrefs.GetInt("Bombs")-1);
        PlayerPrefs.Save();
        _movement.AttemptFall();
        SoundManager.instance.PlaySingle(_explosionSfx);
        InitializeRunTimeBombs();
    }
}
