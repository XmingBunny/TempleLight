using UnityEngine;
using System.Collections;

public class AnimationController
{
    protected Animation animation;

    public AnimationController(Animation animation)
    {
        this.animation = animation;
    }

    public void SetAnimationStatus(Status status)
    {
        if (status != Status.Idle && status != Status.Run)
            IsPlaying = true;
        else
            IsPlaying = false;
        animation.CrossFade(status.ToString());
    }

    public float GetNormalizedTime()
    {
        return 0.8f;
    }

    public bool IsPlaying;
}

public enum Status
{
    Idle,
    Walk,
    Run,
    Attack2,
    Attack4,
    Fire,
    AttackReady,
    Idle2,
    Run2
}
