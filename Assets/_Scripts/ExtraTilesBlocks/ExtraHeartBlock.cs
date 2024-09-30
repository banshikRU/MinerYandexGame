using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeartBlock : IAmBlock
{
    public override void DestroyMe()
    {
        base.DestroyMe();
        HeartManager.hitMeInstance.Invoke(-1,false);
    }
}
