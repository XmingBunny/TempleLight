using UnityEngine;
using System.Collections;

public class TestSKill : MonoBehaviour
{
    public UnitController unitController;
    public Skill _skill;

    void Start()
    {
        //unitController.skillController.CurSkill = new Skill();
        //_skill = unitController.skillController.CurSkill;

        //ActiveEvent a1 = new ActiveEvent();

        //AnimationAction an = new AnimationAction();
        //an.state = Status.Attack2;
        //FinishAction fa = new FinishAction();
        //fa.ExistTime = 2.0f;
        //ParticleAction pa = new ParticleAction();
        //pa.ExistTime = 2.0f;
        //pa.Particle = Resources.Load<GameObject>("Prefabs/Blue");
        //a1.actions.Add(an);
        //a1.actions.Add(fa);
        //a1.actions.Add(pa);

        //_skill.ActiveEvents.Add(a1);
    }

    public void TestSkill()
    {
        unitController.skillController.Excute(0);
    }
}
