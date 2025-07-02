using UnityEngine;

// without abstract here it would throw in error
// this allows this class to not do what is required from state class
// passes those responsibilities to the next class that inherits this one
public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

}
