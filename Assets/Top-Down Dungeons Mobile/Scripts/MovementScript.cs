using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    public float speed = 10.0f;
    public UnitController unitController;
    public CharacterController cc;

    protected AnimationController aniC;

    void Start()
    {
        aniC = unitController.animationController;
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        move.x = -Input.GetAxis("Horizontal") * speed;
        move.z = -Input.GetAxis("Vertical") * speed;

        if (move != Vector3.zero && !aniC.IsPlaying)
        {
            aniC.SetAnimationStatus(Status.Run);
            transform.rotation = Quaternion.LookRotation(move, Vector3.up);
            cc.SimpleMove(move);
        }
        else if (!aniC.IsPlaying)
        {
            aniC.SetAnimationStatus(Status.Idle);
        }



    }
}
