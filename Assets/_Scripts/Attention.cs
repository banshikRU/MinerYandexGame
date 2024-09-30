using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    [SerializeField] private float _timeToDelete;
    private void FixedUpdate()
    {
        _timeToDelete -= Time.fixedDeltaTime;
        if (_timeToDelete <= 0)
        {
            Destroy(gameObject);
        }
    }
}
