using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : IAmBlock
{
    [SerializeField] private GameObject _collectableOre;
    public override void DestroyMe()
    {
        base.DestroyMe();
        Instantiate(_collectableOre, transform.position, Quaternion.identity);
    }
}
