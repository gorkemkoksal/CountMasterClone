using DG.Tweening;
using System.Collections;
using UnityEngine;
public class ClimbState : PlayerBaseState
{
    private int _totalUnits;
    private int _extraUnits;
    private int _numberOfRows;
    private float _prizeCounter=0.2f;

    public ClimbState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.transform.DOMove(new Vector3(0, stateMachine.transform.position.y, stateMachine.transform.position.z), 1f);  //

        SetTeamLeaderTheVirtualCam();

        stateMachine.PassingVirtualCam.gameObject.SetActive(true);
        stateMachine.transform.GetChild(0).gameObject.SetActive(false);

        _totalUnits = stateMachine.transform.childCount - 1;

        stateMachine.StartCoroutine(CreateTower());
    }
    public override void FixedTick(float fixedDeltatime)
    {
        Move(stateMachine.VerticalSpeedAtClimb);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.transform.childCount < 2)
        {
            WinMenu(_prizeCounter.ToString() + "x");
        }
    }
    public override void Exit() { }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stair"))
        {
            stateMachine.CinemachineBrain.m_DefaultBlend.m_Time = CineMachineBlendDurationSetter();
            stateMachine.ClimbingVirtualCam.gameObject.SetActive(false);
        }
        else if (other.CompareTag("afterStair"))
        {
            if (stateMachine.transform.childCount < 3) { return; }

            var unitCounter = 1;
            for (int i = stateMachine.transform.childCount - 2; i > 0; i--)
            {
                stateMachine.transform.GetChild(i).DOLocalMove(stateMachine.transform.GetChild(stateMachine.transform.childCount - 1).transform.localPosition - new Vector3(stateMachine.XDistanceInRow * (unitCounter), 0, 0), 1f);
                unitCounter++;
            }
        }
        else if (other.CompareTag("finish"))
        {
            stateMachine.SwitchState(new IdleState(stateMachine));
            WinMenu("!!!10x!!!");
        }
    }
    private void CalculateNumberOfRows()
    {
        int rowCounter = 1;
        var unitNeed = 1;
        while (unitNeed + rowCounter < _totalUnits)
        {
            rowCounter++;
            unitNeed += rowCounter;

        }
        _numberOfRows = rowCounter;
        if (unitNeed != _totalUnits)
        {
            _extraUnits = _totalUnits - unitNeed;
        }
    }
    IEnumerator CreateTower()
    {
        CalculateNumberOfRows();

        var unitPerRow = 0;
        var isItFirstUnit = true;
        var unitCounter = 1;
        float firstUnitInRowPositionX = 0;

        for (int i = 1; i <= _numberOfRows; i++)
        {
            _prizeCounter += 0.4f;

            var isItFirstInTheRow = true;
            unitPerRow++;

            for (int j = 0; j < unitPerRow; j++)
            {
                var unit = stateMachine.transform.GetChild(unitCounter);
                unitCounter++;
                if (isItFirstUnit)
                {
                    isItFirstUnit = false;
                }
                else if (isItFirstInTheRow)
                {
                    isItFirstInTheRow = false;
                    firstUnitInRowPositionX -= stateMachine.XDistanceInRow / 2;
                }
                unit.DOLocalMove(new Vector3(firstUnitInRowPositionX + j * stateMachine.XDistanceInRow,
                    stateMachine.YDistanceForRows * (_numberOfRows - i), 0),
                    1f).SetEase(Ease.Flash);
            }
            yield return new WaitForSeconds(0.05f);
        }
        if (_extraUnits > 0)
        {
            for (int i = 0; i < _extraUnits; i++)
            {
                var unit = stateMachine.transform.GetChild(unitCounter);
                unit.gameObject.SetActive(false);
                unit.transform.parent = null;
            }
        }
        yield return new WaitForSeconds(0.7f);
        stateMachine.ClimbingVirtualCam.gameObject.SetActive(true);
    }
    private float CineMachineBlendDurationSetter()
    {
        var blendTime = 1f;
        for (int i = 0; i < _numberOfRows; i++)
        {
            blendTime += 0.3f;
        }
        return blendTime;
    }
    private void SetTeamLeaderTheVirtualCam()
    {
        stateMachine.PassingVirtualCam.m_Follow = stateMachine.transform.GetChild(1);
        stateMachine.PassingVirtualCam.m_LookAt = stateMachine.transform.GetChild(1);
    }

    private void WinMenu(string prize)
    {
        stateMachine.MenuText.text = $"Congrats you got {prize}";
        stateMachine.AfterLevelCanvas.gameObject.SetActive(true);
    }
}
