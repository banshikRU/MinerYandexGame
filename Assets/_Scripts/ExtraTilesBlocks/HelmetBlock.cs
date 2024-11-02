using System;
public class HelmetBlock : IAmBlock
{
    public override void DestroyMe()
    {
        base.DestroyMe();
        BuffManager.Instance.IsExtraDefenderActive = true;
        if(!BuffManager.Instance.IsKnightActive && !BuffManager.Instance.IsPirateActive)
        {
            AnimationController.Instance.SetAnimatorHelmet();
        }
        HeartManager.Instance._extraHelmetHeart.SetActive(true);
    }
}
