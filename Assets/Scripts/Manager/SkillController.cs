using UnityEngine;
using System;
using System.Collections.Generic;

public class SkillController
{
    Dictionary<int, Skill> SkillTable = new Dictionary<int, Skill>();
    protected UnitController unitController;

    public Skill CurSkill;

    public void Update()
    {
        if (CurSkill != null)
            CurSkill.Update();
    }

    public SkillController(UnitController unitController)
    {
        this.unitController = unitController;
    }

    void SetSkill(int ID)
    {
        try
        {
            //if (!SkillTable.TryGetValue(ID, out CurSkill))
            //{
                CurSkill = Resources.Load<Skill>("FightData/Skill/" + ID + "/Skill");
                SkillTable.Add(ID, CurSkill);
            //}
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void Excute(int SkillID)
    {
        SetSkill(SkillID);

        if (CurSkill != null)
        {
            CurSkill.unitController = this.unitController;
            CurSkill.Use();
        }
    }
}
