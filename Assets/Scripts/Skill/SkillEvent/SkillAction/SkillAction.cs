using UnityEngine;
using System.Collections;

public class SkillAction : ScriptableObject
{
    protected Skill _Skill;

    public virtual void Init(Skill skill)
    {
        this._Skill = skill;
    }

    public virtual void Execute()
    { ;}

    public virtual void Update()
    { ;}

    public virtual void Finish()
    { ;}
}
