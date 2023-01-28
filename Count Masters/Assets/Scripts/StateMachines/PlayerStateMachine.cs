using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public TextMeshProUGUI MenuText;
    [field: SerializeField] public Canvas AfterLevelCanvas { get; private set; }
    [field: SerializeField] public TouchController TouchController { get; private set; }
    [field: SerializeField] public Rigidbody RbMain { get; private set; }
    [field: SerializeField] public TextMeshPro UnitCounterText { get; private set; }
    [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera PassingVirtualCam { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera ClimbingVirtualCam { get; private set; }
    [field: SerializeField] public CinemachineBrain CinemachineBrain { get; private set; }
    [field: SerializeField] public PlayerUnitPool ObjectPool { get; private set; }
    [field: SerializeField] public float HorizontalSpeed { get; private set; } = 10f;
    [field: SerializeField] public float VerticalSpeed { get; private set; } = 10f;
    [field: SerializeField] public float VerticalSpeedAtClimb { get; private set; } = 2f;
    [field: SerializeField] public float MovementSpeedBeforeAttack { get; set; } = 1f;
    [field: SerializeField] public float MovementSpeedAtAttack { get; private set; } = 0.1f;
    [field: SerializeField] public float DistanceFactor { get; private set; } = 0.15f;
    [field: SerializeField] public float Radius { get; private set; } = 1f;
    [field: SerializeField] public float XDistanceInRow { get; private set; } = 1.65f;
    [field: SerializeField] public float YDistanceForRows { get; private set; } = 1f;
    [field: SerializeField] public float XBorderDeadZone { get; private set; } = 0.1f;

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        CinemachineBrain.m_DefaultBlend.m_Time = 2f;
        SwitchState(new IdleState(this));
    }
    public void Printer(float value, string stringer)
    {
        print($"{stringer}" + value);
    }
}