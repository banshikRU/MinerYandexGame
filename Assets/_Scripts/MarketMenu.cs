using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class MarketMenu : MonoBehaviour
{
    [SerializeField] private SmothMovement _smothMovement;
    [SerializeField] private GameObject _marketGroup;
    [SerializeField] private CoinManager _coinsManager;
    [SerializeField] private MarketProduct _firstSkin;
    [SerializeField] private MarketProduct _secondSkin;
    [SerializeField] private MarketProduct _thirdSkin;
    bool _isFirstInitialized;
    private void Start()
    {
        CheckButtonsToBuy();
        _isFirstInitialized = true;
    }
    private void OnEnable()
    {
        if (_isFirstInitialized)
        {
            CheckButtonsToBuy();
        } 
    }
    public void Initialize()
    {
        //_firstSkin.isBuy = PlayerProgress.instance.playerInfo.isFirstScinBuy;
        //_secondSkin.isBuy = PlayerProgress.instance.playerInfo.isSecondScinBuy;
        //_thirdSkin.isBuy = PlayerProgress.instance.playerInfo.isThirdScinBuy;
        _firstSkin.isBuy = PlayerPrefs.GetInt("FirstScinBuy");
        _secondSkin.isBuy = PlayerPrefs.GetInt("SecondScinBuy");
        _thirdSkin.isBuy = PlayerPrefs.GetInt("ThirdScinBuy");
    }
    private void CheckButtonsToBuy()
    {
        List<MarketProduct> list = gameObject.GetComponentsInChildren<MarketProduct>().ToList();
        foreach (MarketProduct product in list)
        {
            if (product.isBuy == 0)
            {
                if (_coinsManager.CoinsCount < product.CoinsNeded || product.isSelected)
                {
                    product.buyButton.interactable = false;
                }
                else
                {
                    if (!product.isSelected)
                    {
                        product.buyButton.interactable = true;
                    }
                }
            }
            else
            {
                product._coins.SetActive(false);
                if (product.isSelected)
                {
                    product.buyButton.interactable = true;
                }
                
            }
        }
    }
    private void BuyProduct(int value,MarketProduct product)
    {
        if (product.isBuy == 0)
        {
            _coinsManager.SubstructCoins(value);
            CheckButtonsToBuy();
        }
        
    }
    private void ResetSkin(MarketProduct _skin)
    {
        _skin.isSelected = false;
        if (_skin.isBuy == 1)
        {
            _skin.buyButton.interactable = true;
        }
    }
    public void SetAnimatorPirate(int value)
    {
        _smothMovement.SetAnimatorPirate();
        BuffManager.instance.IsPirateActive = true;
        ResetSkin(_secondSkin);
        //PlayerProgress.instance.playerInfo.isFirstScinBuy = true;
        //PlayerProgress.instance.Save();
        PlayerPrefs.SetInt("FirstScinBuy", 1);
        PlayerPrefs.Save();
        BuyProduct(value, _firstSkin);
    }
    public void SetAnimatorKnight(int value)
    {

        _smothMovement.SetAnimatorKnight();
        BuffManager.instance.IsKnightActive = true;
        ResetSkin(_firstSkin);
        //PlayerProgress.instance.playerInfo.isSecondScinBuy = true;
        //PlayerProgress.instance.Save();
        PlayerPrefs.SetInt("SecondScinBuy", 1);
        PlayerPrefs.Save();
        BuyProduct(value, _secondSkin);
    }
    public void SetMegaPickaxe(int value)
    {
        BuffManager.instance.IsMegaPickaxeActive = true;
        //PlayerProgress.instance.playerInfo.isThirdScinBuy = true;
        //PlayerProgress.instance.Save();
        PlayerPrefs.SetInt("ThirdScinBuy", 1);
        PlayerPrefs.Save();
        BuyProduct(value, _thirdSkin);
    }
}