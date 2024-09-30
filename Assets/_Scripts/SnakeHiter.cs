using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHiter : MonoBehaviour
{
    private bool _isFirstHit;
    private void Start()
    {
        _isFirstHit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !_isFirstHit)
        {
            _isFirstHit =true;
            HeartManager.hitMeInstance.Invoke(1, false);
        }
    }
}
