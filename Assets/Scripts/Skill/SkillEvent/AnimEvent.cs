using UnityEngine;
using System.Collections;

public class AnimEvent : SkillEvent
{
    public float WaitTime;

    private float timer = 0f;
    private bool hasExecute = false;

    public override void Update()
    {
        if (timer < WaitTime)
        {
            timer += Time.deltaTime;
            return;
        }

        if (!hasExecute)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Execute();
            }
        }
        hasExecute = true;
    }

    public override void Reset()
    {
        timer = 0f;
        hasExecute = false;
    }
}
