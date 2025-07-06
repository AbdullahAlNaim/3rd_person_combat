using System;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    
    private float previousFrameTime;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            
        }

        previousFrameTime = normalizedTime;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    public override void Exit()
    {

    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
        
    }
}
