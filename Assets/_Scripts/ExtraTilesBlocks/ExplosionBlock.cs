using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionBlock : IAmBlock
{
    [SerializeField] private int _levelDestroy;
    [SerializeField] private LayerMask _floor;
    public override void DestroyMe()
    {
        base.DestroyMe();
        HeartManager.hitMeInstance.Invoke(1, false);
        HitAllBlock();

    }
    private void HitAllBlock()
    {
        List<RaycastHit2D> blocks;
        blocks = Physics2D.RaycastAll(transform.position, new Vector2(0, -1), _levelDestroy,_floor).ToList();
        foreach (RaycastHit2D block in blocks)
        {
            Debug.Log(block);
            block.transform.gameObject.GetComponent<IAmBlock>().ExplosiveDestroy();
        }
    }
}
