using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{
    [SerializeField]private Image _healBar;
    public void UpdateHealBar(float fillAmount)
    {
        _healBar.fillAmount = fillAmount;
    }
}
