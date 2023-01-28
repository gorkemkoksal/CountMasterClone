using TMPro;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public TextMeshProUGUI MenuText;
    [field: SerializeField] public Canvas AfterGameCanvas { get; private set; }
    [field: SerializeField] public float UnitSpeedAtAttack { get; private set; } = 0.6f;
    [field: SerializeField] public TextMeshPro UnitCounterText { get; private set; }
    [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    [field: SerializeField] public float DistanceFactor { get; private set; } = 0.05f;
    [field: SerializeField] public float Radius { get; private set; } = 1f;
    public Quaternion EnemyRotation { get; private set; } = new Quaternion(0, 180, 0, 1);
    private void Start()
    {
        SwitchState(new EnemyIdleState(this));
    }
}
