using UnityEngine;

public abstract class State // abstract means cant create but can inherit/use
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}
