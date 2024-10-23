using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanguageSwitch : UnityEvent<string> {}
public class Language : Singleton<Language>
{
    private string _curentLanguage;
    public static LanguageSwitch lanSwitch;

    public string CurentLanguage { get => _curentLanguage; set => _curentLanguage = value; }

    protected override void OnInit()
    {
        base.OnInit();
        lanSwitch = new LanguageSwitch();
    }
    public void Initialize()
    {
    #if !UNITY_EDITOR
       CurentLanguage = YandexManager.ysdk.GetLanguage();
    #endif
    }
    public void LanguageSwitch()
    {
        lanSwitch.Invoke(CurentLanguage);
    }
}
