using UnityEngine;
using System.Collections;

public class FinishAction : SkillAction
{
    public Status state = Status.Idle;
    public float ExistTime = 1.0f;

    public override void Execute()
    {
        _Skill.unitController.InvokeCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(ExistTime);
        EndSkill();
    }

    public void EndSkill()
    {
        _Skill.unitController.animationController.SetAnimationStatus(state);
        _Skill.Finish();
    }
}
