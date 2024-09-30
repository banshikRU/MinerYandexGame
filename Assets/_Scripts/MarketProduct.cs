using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketProduct : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] public GameObject _coins;
    [SerializeField] private int _coinsNeded;
    [NonSerialized]public Button buyButton;
    [NonSerialized]public bool isSelected;
    [NonSerialized] public int isBuy;
    public int CoinsNeded { get => _coinsNeded;}

    private void Awake()
    {
        isSelected = false;
        buyButton = gameObject.GetComponent<Button>();
        _coinsCount.text = _coinsNeded.ToString();
    }
    public void OffCoins()
    {
        if (isBuy == 0)
        {
            _coins.SetActive(false);
            isSelected = true;
            isBuy = 1;
            buyButton.interactable = false;
        }
        else
        {
            isSelected = true;
            buyButton.interactable = false;
        }
    }
}
