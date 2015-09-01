using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    public float speed = 10.0f;
    public CharacterAnimationController cac;
    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        move.x = -Input.GetAxis("Horizontal") * speed;
        move.z = -Input.GetAxis("Vertical") * speed;

        if (move != Vector3.zero && !cac.IsPlaying)
        {
            cac.SetRun();
            transform.rotation = Quaternion.LookRotation(move, Vector3.up);
            cc.SimpleMove(move);
        }
        else
        {
            cac.SetIdle();
        }



    }
}
