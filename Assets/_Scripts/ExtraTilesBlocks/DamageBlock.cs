using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : IAmBlock
{
    [SerializeField] private GameObject _myBuff;
    public override void DestroyMe()
    {
        base.DestroyMe();
        BuffVisualManager.buffCreate.Invoke(_myBuff);
    }
}
