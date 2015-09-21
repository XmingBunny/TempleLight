using UnityEngine;
using System.Collections;


public class UnitController : MonoBehaviour
{
    [HideInInspector]
    public SkillController skillController;
    public AnimationController animationController;

    void Awake()
    {
        skillController = new SkillController(this);
        animationController = new AnimationController(animation);
    }

    void Update()
    {
        skillController.Update();
    }

    public void InvokeCoroutine(IEnumerator a)
    {
        StartCoroutine(a);
    }
}

