using UnityEngine;
using System.Collections;

public class AnimationAction : SkillAction
{
    public Status state;

    public override void Execute()
    {
        _Skill.unitController.animationController.SetAnimationStatus(state);
    }
}
