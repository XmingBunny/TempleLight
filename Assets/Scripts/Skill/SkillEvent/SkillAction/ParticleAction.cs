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
        gameObject.transform.position = CoordinateTransitionToWorld(_Skill.unitController.transform, Position);

        particleController = gameObject.GetComponent<ParticleController>();
        particleController.ExistTime = ExistTime;
        particleController.StartTimer();
    }

    //将相对于unit的坐标转化为世界坐标
    Vector3 CoordinateTransitionToWorld(Transform parent, Vector3 RelativePosition)
    {
        float Radian = parent.eulerAngles.y / 180f * Mathf.PI;
        float x = RelativePosition.x * Mathf.Cos(Radian) + RelativePosition.z * Mathf.Sin(Radian);
        float y = RelativePosition.y;
        float z = -RelativePosition.x * Mathf.Sin(Radian) + RelativePosition.z * Mathf.Cos(Radian);

        Vector3 result = new Vector3(x, y, z) + parent.transform.position;

        return result;
    }
}

