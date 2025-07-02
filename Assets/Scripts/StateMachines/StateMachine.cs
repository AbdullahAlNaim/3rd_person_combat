using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{

    private State currentState;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime);
        }
    }

    public void SwitchState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
   }
}
