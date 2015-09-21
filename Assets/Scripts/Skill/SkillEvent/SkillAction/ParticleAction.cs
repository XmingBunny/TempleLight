using UnityEngine;
using System.Collections;

public class ParticleAction : SkillAction
{
    public GameObject Particle;
    public float ExistTime = 1.0f;
    public Vector3 Position = new Vector3();

    protected ParticleController particleController;
    protected GameObject gameObject;

    public override void Execute()
    {
        gameObject = (GameObject)GameObject.Instantiate(Particle);
        gameObject.transform.parent = _Skill.unitController.transform;
        gameObject.transform.localPosition = Position;
        gameObject.transform.rotation = _Skill.unitController.transform.rotation;
        //gameObject.transform.parent = null;
        particleController = gameObject.GetComponent<ParticleController>();
        particleController.ExistTime = ExistTime;
        particleController.StartTimer();
    }
}

