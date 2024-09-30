using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneRain : MonoBehaviour
{
    [SerializeField] private Sprite _mySpriteEn;
    [SerializeField] private Sprite _mySpriteRu;
    private Animator _myAnimator;
    private Image _curentSprite;
    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
        _curentSprite = GetComponent<Image>();
        Language.lanSwitch.AddListener(SwitchAnimation);
    }
    private void OnEnable()
    {
        Language.instance.LanguageSwitch();
        StartCoroutine(StoneRainn());
    }
    IEnumerator StoneRainn()
    {
        _myAnimator.Play("StoneRain");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }
    private void SwitchAnimation(string lan)
    {
        Debug.Log(lan);
        switch (lan)
        {
            case "ru":
                _curentSprite.sprite = _mySpriteRu;
                break;
            case "en":
                _curentSprite.sprite = _mySpriteEn;
                break;
            default:
                _curentSprite.sprite = _mySpriteEn;
                break;
        }
    }
}
