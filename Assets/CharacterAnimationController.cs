using UnityEngine;
using System.Collections;

public class CharacterAnimationController : MonoBehaviour
{
    public Transform weapon;
    bool isPlaying = false;

    public bool IsPlaying
    {
        get { return isPlaying; }
    }

    public void SetIdle()
    {
        if (isPlaying)
            return;

        animation.CrossFade("Idle");
    }

    public void SetIdle2()
    {
        if (isPlaying)
            return;

        animation.CrossFade("Idle2");
    }

    public void SetWalk()
    {
        if (isPlaying)
            return;

        weapon.renderer.enabled = true;
        animation.CrossFade("Walk");
    }

    public void SetWalk2()
    {
        if (isPlaying)
            return;

        weapon.renderer.enabled = false;
        animation.CrossFade("Walk2");
    }

    public void SetRun()
    {
        if (isPlaying)
            return;

        weapon.renderer.enabled = true;
        animation.CrossFade("Run");
    }

    public void SetRun2()
    {
        if (isPlaying)
            return;

        weapon.renderer.enabled = false;
        animation.CrossFade("Run2");
    }

    public void SetJump()
    {
        weapon.renderer.enabled = true;
        animation.CrossFade("Jump");
    }

    public void SetJump2()
    {
        weapon.renderer.enabled = false;
        animation.CrossFade("Jump2");
    }

    public void SetAttackReady()
    {
        SetAnimation("AttackReady");
    }

    public void SetAttack1()
    {
        SetAnimation("Attack1");
    }

    public void SetAttack2()
    {
        SetAnimation("Attack2");
    }

    public void SetAttack3_1()
    {
        SetAnimation("Attack3-1");
    }

    public void SetAttack3_2()
    {
        SetAnimation("Attack3-2");
    }

    public void SetAttack3_3()
    {
        SetAnimation("Attack3-3");
    }

    public void SetAttack4()
    {
        SetAnimation("Attack4", 3f);
    }

    public void SetWound()
    {
        SetAnimation("Wound");
    }

    public void SetStun()
    {
        SetAnimation("Stun");
    }

    public void SetHitAway()
    {
        SetAnimation("HitAway");
    }

    public void SetHitAwayUp()
    {
        SetAnimation("HitAwayUp");
    }

    public void SetDeath()
    {
        SetAnimation("Death");
    }

    public void SetMagic()
    {
        SetAnimation("Magic");
    }

    public void SetFire()
    {
        SetAnimation("Fire");
    }

    IEnumerator Release(float second)
    {
        yield return new WaitForSeconds(second);

        isPlaying = false;
    }


    void SetAnimation(string ani, float second = 1)
    {
        if (!isPlaying)
        {
            isPlaying = true;

            animation.CrossFade(ani);

            StartCoroutine(Release(second));
        }

    }
}
