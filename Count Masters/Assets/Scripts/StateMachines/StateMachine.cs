using UnityEngine;
public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        currentState?.FixedTick(Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        currentState?.OnTriggerEnter(other);
    }
}
