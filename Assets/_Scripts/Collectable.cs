using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private OreTypeEnum _oreType;
    private Transform _target; 
    [SerializeField]private float speed = 1.0f; 
    [SerializeField]private int _price;
    private void Start()
    {
        if (BuffManager.instance.IsExtraExtractionActive)
        {
            _price *= 2;
        }
        DamagePopup.Create(gameObject.transform.position + new Vector3(0, 0.5f),_price, BuffManager.instance.IsExtraExtractionActive);
        _target = GameObject.Find("Target").transform;
        ScoreManager._scoreEvent.Invoke(_price);
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, _target.position, step);
    }
}
