using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.TouchController.OnStart += OnGameStart;

        BoolAnimParameterSetterForAllUnits(stateMachine.transform,IS_RUNNING_HASH, false);
        CountAndSetCounterText(stateMachine.transform,stateMachine.UnitCounterText);
    }
    public override void FixedTick(float fixedDeltatime){}
    public override void Tick(float deltaTime){}
    public override void Exit() 
    {
        BoolAnimParameterSetterForAllUnits(stateMachine.transform, IS_RUNNING_HASH, true);

        stateMachine.TouchController.OnStart -= OnGameStart;
    }
    public override void OnTriggerEnter(Collider other) { }
    private void OnGameStart()
    {
        stateMachine.SwitchState(new RunState(stateMachine));
    }

}
