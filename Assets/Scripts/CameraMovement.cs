using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float Speed;

    private CameraState cameraState = CameraState.Player;
    Vector3[] points;
    protected Transform player;
    Vector3 aboveVector;

    public CameraState CameraState
    {
        get { return cameraState; }
        set { cameraState = value; }
    }

    void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).transform;
        points = new Vector3[5];
        aboveVector = new Vector3(0f, 6, 0f);

        Vector3 v = new Vector3(player.position.x, aboveVector.y, player.position.z) - transform.position;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = (i * 0.25f) * v;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (CameraState)
        {
            case global::CameraState.Player:
                SmoothMovement();
                SmoothLookAt();
                break;
        }
    }

    void SmoothMovement()
    {
      Vector3  targetPoint = points[4];
        for (int i = points.Length - 1; i >= 0; i--)
        {
            Vector3 point = new Vector3(player.position.x, aboveVector.y, player.position.z) + points[i];
            if (CanSeePlayer(point))
            {
                targetPoint = point;
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * Speed);
    }

    void SmoothLookAt()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * Speed);
    }

    bool CanSeePlayer(Vector3 position)
    {
        bool b = false;
        RaycastHit raycastHit = new RaycastHit();
        Vector3 direction = player.position - position;

        if (Physics.Raycast(position, direction, out raycastHit))
        {
            if (raycastHit.collider.gameObject.tag == Tags.Player)
                return true;
        }

        return b;
    }
}

public enum CameraState
{
    Player = 1,
    NPC = 2,
    SkillAnim = 3,
    Null = -1
}
