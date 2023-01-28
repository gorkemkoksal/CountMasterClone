using UnityEngine;
public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
        CreateUnits(Random.Range(20, 25), enemyStateMachine.UnitPrefab, enemyStateMachine.EnemyRotation, enemyStateMachine.transform); //buraya bi rakamlari variable yap

        CountAndSetCounterText(enemyStateMachine.transform, enemyStateMachine.UnitCounterText);

        FormatUnits(enemyStateMachine.transform, enemyStateMachine.DistanceFactor, enemyStateMachine.Radius, true);
    }
    public override void FixedTick(float fixedDeltatime) { }

    public override void Tick(float deltaTime) { }
    public override void Exit() { }
    public override void OnTriggerEnter(Collider other) { }
}
