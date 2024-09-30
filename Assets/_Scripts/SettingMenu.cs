using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu instance;
    [SerializeField] private Image _soundButton;
    [SerializeField] private Image _sfxButtonSlider;
    [SerializeField] private Sprite _oNSlider; 
    [SerializeField] private Sprite _offSlider;
    [SerializeField] private Button _russianLanButton;
    [SerializeField] private Button _englishLanButton;
    private bool _isSoundOn;
    private bool _isSfxOn;
    private void Awake()
    {
        _isSoundOn = true;
        _isSfxOn = true;
        instance = this;
    }
    private void OnEnable()
    {
        if (Language.instance.CurentLanguage == "ru")
        {
            _russianLanButton.interactable = false;
            _englishLanButton.interactable = true;
        }
        else if (Language.instance.CurentLanguage == "eng")
        {
            _russianLanButton.interactable = true;
            _englishLanButton.interactable = false;
        }
    }
    public void OnOffSound()
    {
        if (_isSoundOn)
        {
            _isSoundOn = false;
            SoundManager.instance.OnOffAllSound(_isSoundOn);
            _soundButton.sprite = _offSlider;
        }
        else
        {
            _isSoundOn = true;
            SoundManager.instance.OnOffAllSound(_isSoundOn);
            _soundButton.sprite = _oNSlider;
        }
    }
    public void OnOffSfx()
    {
        if (_isSfxOn)
        {
            _isSfxOn = false;
            SoundManager.instance.OnOffAllSfx(_isSfxOn);
            _sfxButtonSlider.sprite = _offSlider;
        }
        else
        {
            _isSfxOn = true;
            SoundManager.instance.OnOffAllSfx(_isSfxOn);
            _sfxButtonSlider.sprite = _oNSlider;
        }
    }
    public void SwitchLanguageRus()
    {
        Language.instance.CurentLanguage = "ru";
        Language.instance.LanguageSwitch();
        _russianLanButton.interactable = false;
        _englishLanButton.interactable = true;
    }
    public void SwitchLanguageEng()
    {
        Language.instance.CurentLanguage = "eng";
        Language.instance.LanguageSwitch();
        _russianLanButton.interactable = true;
        _englishLanButton.interactable = false;
    }
}
