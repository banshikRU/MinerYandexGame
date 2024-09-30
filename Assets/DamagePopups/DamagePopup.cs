using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float _moveYSpeed;
    private float _disappearTimer;
    private TextMeshPro _textMesh;
    private Color _textColor;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private Vector3 _moveVector;
    private static int sortingOrder;
    public static DamagePopup Create(Vector3 position, float damageAmount,bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount ,isCriticalHit);

        return damagePopup;
    }
    private void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(float damageAmount, bool isCriticalHit)
    {
        _textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            _textMesh.fontSize = 4f;
            _textColor = GetColorFromString("FFC500");
        }
        else
        {
            _textMesh.fontSize = 7f;
            _textColor = GetColorFromString("DC143C");
        }
        _textMesh.color = _textColor;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        _textMesh.sortingOrder = sortingOrder;
        _moveVector = new Vector3(Random.Range(-1f,2f), Random.Range(1,3)*_moveYSpeed);
    }
    private void Update()
    {
        
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * 8f * Time.deltaTime;
        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer >DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        if (_disappearTimer<0)
        {
            float disappearSpeed = 2f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a <0 )
            {
                Destroy(gameObject);
            }
        }
    }
    public Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }
    public float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }
    public int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }
}
