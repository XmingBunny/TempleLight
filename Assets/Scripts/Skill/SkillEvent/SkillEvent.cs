using UnityEngine;
using System.Collections.Generic;

public class SkillEvent : ScriptableObject 
{
    protected Skill _Skill;

    public List<SkillAction> actions = new List<SkillAction>();

    public virtual void Init(Skill skill)
    {
        this._Skill = skill;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Init(skill);
        }
    }

    public virtual void Execute()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Execute();
        }
    }

    public virtual void Update()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Update();
        }
    }

    public virtual void Finish()
    { }

    public virtual void Reset()
    { }
}
