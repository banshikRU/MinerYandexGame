using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    [TextArea(0,4)]
    [SerializeField]private string _ru;
    [TextArea(0, 4)]
    [SerializeField]private string _eng;
    private TextMeshProUGUI _localizationText;
    private void Awake()
    {
        _localizationText = GetComponent<TextMeshProUGUI>();
        Language.lanSwitch.AddListener(SwitchLanguage);
    }
    private void SwitchLanguage(string lang)
    {
        if (lang == "ru")
        {
            _localizationText.text = _ru;
        }
        else
        {
            _localizationText.text = _eng;
        }
    }
    private void OnEnable()
    {
        Language.instance.LanguageSwitch();
    }

}
