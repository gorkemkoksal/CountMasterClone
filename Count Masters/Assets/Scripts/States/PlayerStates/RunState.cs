using System.Collections;
using UnityEngine;
public class RunState : PlayerBaseState
{
    private Vector3 _horizontalDirection;
    private float _xBorder;
    private bool _isDead;
    public RunState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter() { }
    public override void FixedTick(float fixedDeltatime)
    {
        if (_isDead) { return; }

        _horizontalDirection = new Vector3(stateMachine.TouchController.TouchDirectionOnX, 0, 0);
        SetBorders();

        Move(_horizontalDirection, stateMachine.VerticalSpeed);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.transform.childCount < 2)
        {
            _isDead = true;
            stateMachine.StartCoroutine(LooseMenuDisplay());
        }
        CountAndSetCounterText(stateMachine.transform, stateMachine.UnitCounterText);
    }
    public override void Exit() { }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            UnitCreationAccordingToGate(other);
        }
        else if (other.CompareTag("enemy"))
        {
            var enemyStateMachine = other.GetComponent<EnemyStateMachine>();
            enemyStateMachine.SwitchState(new EnemyAttackState(enemyStateMachine, stateMachine.transform));

            stateMachine.SwitchState(new AttackState(stateMachine, other.transform));
        }
        else if (other.CompareTag("tower"))
        {
            stateMachine.SwitchState(new ClimbState(stateMachine));
        }
    }
    private void SetBorders()
    {
        var numberOfUnits = stateMachine.transform.childCount - 1;

        if (numberOfUnits > 200)
            _xBorder = 0.1f;
        else if (numberOfUnits > 150)
            _xBorder = 0.3f;
        else if (numberOfUnits > 100)
            _xBorder = 0.5f;
        else if (numberOfUnits > 75)
            _xBorder = 0.75f;
        else if (numberOfUnits > 50)
            _xBorder = 1f;
        else if (numberOfUnits > 20)
            _xBorder = 1.15f;
        else if (numberOfUnits > 10)
            _xBorder = 1.3f;
        else
            _xBorder = 1.5f;

        if ((stateMachine.transform.position.x > _xBorder - stateMachine.XBorderDeadZone && _horizontalDirection.x > 0) ||
            (stateMachine.transform.position.x < -_xBorder + stateMachine.XBorderDeadZone && _horizontalDirection.x < 0))
        {
            _horizontalDirection = Vector3.zero;
        }
    }
    private void UnitCreationAccordingToGate(Collider other)
    {
        var numberOfUnits = stateMachine.transform.childCount - 1;
        var gateDisabler = other.transform.parent.GetComponent<GateDisabler>();
        gateDisabler.DisableGateColliders();

        var gateManager = other.GetComponent<GateManager>();

        if (gateManager.IsMultiply)
        {
            ForLoopForUnitCreation(numberOfUnits * gateManager.RandomNumber - numberOfUnits);
        }
        else
        {
            ForLoopForUnitCreation(gateManager.RandomNumber);
        }

        BoolAnimParameterSetterForAllUnits(stateMachine.transform, IS_RUNNING_HASH, true);
        FormatUnits(stateMachine.transform, stateMachine.DistanceFactor, stateMachine.Radius, false);
        CountAndSetCounterText(stateMachine.transform, stateMachine.UnitCounterText);
    }
    IEnumerator LooseMenuDisplay()
    {
        yield return new WaitForSeconds(1f);
        stateMachine.MenuText.text = "Sorry, you lost all of your units";
        stateMachine.AfterLevelCanvas.gameObject.SetActive(true);
    }
    private void ForLoopForUnitCreation(int count)
    {
        for(int i=0; i<count; i++)
        {
            var unit = stateMachine.ObjectPool.Pool.Get();
            unit.transform.parent = stateMachine.transform;
            unit.transform.position=stateMachine.transform.localPosition;
        }
    }
}
