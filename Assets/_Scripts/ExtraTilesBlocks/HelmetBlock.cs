using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetBlock : IAmBlock
{
    HeartManager _heartManager;
    SmothMovement _playerMover;
    public override void DestroyMe()
    {
        base.DestroyMe();
        _playerMover = FindObjectOfType<SmothMovement>();
        _heartManager = FindObjectOfType<HeartManager>();
        BuffManager.instance.IsExtraDefenderActive = true;
        if(!BuffManager.instance.IsKnightActive && !BuffManager.instance.IsPirateActive)
        {
            _playerMover.SetAnimatorHelmet();
        }
        _heartManager._extraHelmetHeart.SetActive(true);
    }
}
