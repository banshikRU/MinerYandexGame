using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HitMe : UnityEvent<int,bool> { }
public class HeartManager : MonoBehaviour
{
    [SerializeField] private SmothMovement _playerMover;
    public static HitMe hitMeInstance = new HitMe();
    [SerializeField] private AudioClip _playerTakeHit;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _firstHeart;
    [SerializeField] private GameObject _secondHeart;
    [SerializeField] private GameObject _thirdHeart;
    public GameObject _extraHelmetHeart;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    public bool isRainRockActive;
    private int _curentHealth;
    public int _takeRocks;
    private void Start()
    {
        _extraHelmetHeart.SetActive(false);
        _curentHealth = 3;
        hitMeInstance.AddListener(HitMe);
    }
    private void HitMe(int hit,bool isRockAttack)
    {
        _playerMover.HitMe();
        SoundManager.instance.PlaySingle(_playerTakeHit);
        if (BuffManager.instance.IsExtraDefenderActive && isRockAttack)
        {
            if (!BuffManager.instance.IsKnightActive && !BuffManager.instance.IsPirateActive)
            {
                _playerMover.SetAnimatorWithoutHelmet();
            }
            _extraHelmetHeart.SetActive(false);
            BuffManager.instance.IsExtraDefenderActive = false;
        }
        else
        {
            if (isRainRockActive)
            {
                _takeRocks += 1;
            }
            _curentHealth -= hit;
            switch (_curentHealth)
            {
                case 0:
                    DeactivateHeart(_firstHeart);
                    DeactivateHeart(_secondHeart);
                    DeactivateHeart(_thirdHeart);
                    Death();
                    break;
                case 1:
                    ActivateHeart(_firstHeart);
                    DeactivateHeart(_secondHeart);
                    DeactivateHeart(_thirdHeart);
                    break;
                case 2:
                    ActivateHeart(_firstHeart);
                    ActivateHeart(_secondHeart);
                    DeactivateHeart(_thirdHeart);
                    break;
                case 3:
                    ActivateHeart(_firstHeart);
                    ActivateHeart(_secondHeart);
                    ActivateHeart(_thirdHeart);
                    break;
            }
        }
    }
    private void ActivateHeart(GameObject heart)
    {
        Image heartImage = heart.GetComponent<Image>();
        heartImage.sprite = _fullHeart;

    }
    private void DeactivateHeart(GameObject heart)
    {
        Image heartImage = heart.GetComponent<Image>();
        heartImage.sprite = _emptyHeart;
    }
    private void Death()
    {
        _gameManager.PlayerDeath();
    }
}
