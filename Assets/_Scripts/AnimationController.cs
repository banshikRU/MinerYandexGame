using System;
using UnityEngine;
using AnimatorController = UnityEngine.RuntimeAnimatorController;

public class AnimationController : Singleton<AnimationController>
{
    public static event Action OnHelmet;
    public static event Action WithoutHelmet;
    public static event Action OnPirate;
    public static event Action OnKnight;

    [SerializeField] private AnimatorController _withHelmet;
    [SerializeField] private AnimatorController _withoutHelmet;
    [SerializeField] private AnimatorController _pirate;
    [SerializeField] private AnimatorController _knight;

    [SerializeField] private Animator _playerAnimator;

    protected override void Awake()
    {
        base.Awake();
    }
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
