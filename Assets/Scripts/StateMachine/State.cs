public class State
{
    public StateMachine owner;

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}