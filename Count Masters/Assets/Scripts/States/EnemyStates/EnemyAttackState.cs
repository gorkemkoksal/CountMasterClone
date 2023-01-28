using System.Collections;
using UnityEngine;
public class EnemyAttackState : EnemyBaseState
{
    private Transform _playerTransform;
    public EnemyAttackState(EnemyStateMachine enemyStateMachine, Transform playerTransform) : base(enemyStateMachine)
    {
        _playerTransform = playerTransform;
    }
    public override void Enter()
    {
        OnLoose += LooseMenu;
        BoolAnimParameterSetterForAllUnits(enemyStateMachine.transform, IS_RUNNING_HASH, true);
    }
    public override void FixedTick(float fixedDeltatime) { }

    public override void Tick(float deltaTime)
    {
        FightProcess(_playerTransform, enemyStateMachine.transform, enemyStateMachine.UnitSpeedAtAttack, true, deltaTime);
        CountAndSetCounterText(enemyStateMachine.transform, enemyStateMachine.UnitCounterText);
    }
    public override void Exit()
    {
        OnLoose -= LooseMenu;
    }
    public override void OnTriggerEnter(Collider other) { }
    private void LooseMenu()
    {
        BoolAnimParameterSetterForAllUnits(enemyStateMachine.transform, IS_RUNNING_HASH, false);
        enemyStateMachine.StartCoroutine(LooseMenuDisplay());
    }
    IEnumerator LooseMenuDisplay()
    {
        yield return new WaitForSeconds(1f);
        enemyStateMachine.MenuText.text = "You are defeated";
        enemyStateMachine.AfterGameCanvas.gameObject.SetActive(true);
    }
}
