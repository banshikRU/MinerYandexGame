using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpPopUp : MonoBehaviour
{
    public Vector3 targetScale = Vector3.zero;
    private Vector3 _startScale;
    private BuffManager _buffManager;
    [SerializeField] private float _remainingTime;
    private void Start()
    {
        _startScale = transform.localScale;
        _buffManager = GameObject.FindObjectOfType<BuffManager>();
        if (_buffManager.IsDoubleBuffTimeActive)
        {
            _remainingTime *= 2;
        }
    }
    void FixedUpdate()
    {
        _buffManager.IsExtraDamageActive = true;
        _remainingTime -= Time.fixedDeltaTime;
        transform.localScale = Vector3.Lerp(targetScale, _startScale, _remainingTime);
        if (_remainingTime<= 0)
            {
                _buffManager.IsExtraDamageActive = false;
                Destroy(gameObject);
            }
    }
}
