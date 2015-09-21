using UnityEngine;
using System.Collections.Generic;

public class Skill : ScriptableObject
{
    public int ID;              //技能ID
    public string desc = "";       //技能描述
  
    public List<ActiveEvent> ActiveEvents = new List<ActiveEvent>();
    public List<AnimEvent> AnimEvents = new List<AnimEvent>();
    [HideInInspector]
    public UnitController unitController;

    protected bool _Excute = false;

    public void Use()
    {
        _Excute = true;
        Reset();

        for (int i = 0; i < ActiveEvents.Count; i++)
        {
            ActiveEvents[i].Init(this);
            ActiveEvents[i].Execute();
        }

        for (int i = 0; i < AnimEvents.Count; i++)
        {
            AnimEvents[i].Init(this);
        }
    }

    public void Update()
    {
        if (!_Excute)
            return;

        for (int i = 0; i < AnimEvents.Count; i++)
        {
            AnimEvents[i].Update();
        }
    }

    public void Finish()
    {
        _Excute = false;


        for (int i = 0; i < ActiveEvents.Count; i++)
        {
            ActiveEvents[i].Finish();
        }

        for (int i = 0; i < AnimEvents.Count; i++)
        {
            AnimEvents[i].Finish();
        }
    }

    public void Interrupt()
    {
        Finish();
    }

    void Reset()
    {
        for (int i = 0; i < ActiveEvents.Count; i++)
        {
            ActiveEvents[i].Reset();
        }

        for (int i = 0; i < AnimEvents.Count; i++)
        {
            AnimEvents[i].Reset();
        }
    }

}
