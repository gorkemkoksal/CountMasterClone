using UnityEngine;
public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine) { this.stateMachine = stateMachine; }
    protected void Move(Vector3 horizontalDirection, float verticalSpeed)
    {
        stateMachine.RbMain.MovePosition(stateMachine.transform.position +
            (Vector3.forward * verticalSpeed +
            horizontalDirection * stateMachine.HorizontalSpeed) * Time.fixedDeltaTime);
    }
    protected void Move(float verticalMovement)
    {
        Move(Vector3.zero, verticalMovement);
    }
}
