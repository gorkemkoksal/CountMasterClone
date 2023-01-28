using UnityEngine;
public class AttackState : PlayerBaseState
{
    private Transform enemyTransform;
    public AttackState(PlayerStateMachine stateMachine, Transform enemyTransform) : base(stateMachine)
    {
        this.enemyTransform = enemyTransform;
    }
    public override void Enter()
    {
        OnAfterAttack += AfterAttack;
    }
    public override void FixedTick(float fixedDeltatime)
    {
        Move(stateMachine.MovementSpeedBeforeAttack);

        if (stateMachine.MovementSpeedBeforeAttack <= 0.25f) { return; }
        stateMachine.MovementSpeedBeforeAttack -= 0.01f;
    }
    public override void Tick(float deltaTime)
    {
        FightProcess(enemyTransform, stateMachine.transform, stateMachine.MovementSpeedAtAttack, false, deltaTime);
        CountAndSetCounterText(stateMachine.transform, stateMachine.UnitCounterText);
    }
    public override void Exit() {
        FormatUnits(stateMachine.transform, stateMachine.DistanceFactor, stateMachine.Radius, false);
       
        OnAfterAttack -= AfterAttack;
    }
    public override void OnTriggerEnter(Collider other) { }

    private void AfterAttack()
    {
        enemyTransform.gameObject.SetActive(false);
        stateMachine.SwitchState(new RunState(stateMachine));
    }
}
