using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorController = UnityEngine.RuntimeAnimatorController;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private AnimatorController _withHelmet;
    [SerializeField] private AnimatorController _withoutHelmet;
    [SerializeField] private AnimatorController _pirate;
    [SerializeField] private AnimatorController _knight;

    public void SetAnimatorHelmet()
    {
        _playerAnimator.runtimeAnimatorController = _withHelmet;
    }
    public void SetAnimatorWithoutHelmet()
    {
        _playerAnimator.runtimeAnimatorController = _withoutHelmet;
    }
    public void SetAnimatorPirate()
    {
        _playerAnimator.runtimeAnimatorController = _pirate;
    }
    public void SetAnimatorKnight()
    {
        _playerAnimator.runtimeAnimatorController = _knight;
    }
}
