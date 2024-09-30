using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBlock :IAmBlock
{
    [SerializeField] private AudioClip _moneyTakeAudio;
    [SerializeField] private int _myPrize;
    public override void DestroyMe()
    {
        SoundManager.instance.PlaySingle(_moneyTakeAudio);
        RunTimeCoinManager.instance.PlusCoins(_myPrize);
        base.DestroyMe();

    }
}
