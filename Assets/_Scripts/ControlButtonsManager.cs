using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtonsManager : MonoBehaviour
{
    [SerializeField] private SmothMovement _movement;
    [SerializeField] private GameObject _upButtons;
    [SerializeField] private GameObject _downButtons;
    private void Update()
    {
        if (_movement._isJump )
        {
            _upButtons.SetActive(false);
            _downButtons.SetActive(true);
        }
        else
        {
            _upButtons.SetActive(true);
            _downButtons.SetActive(false);
        }
    }
}
