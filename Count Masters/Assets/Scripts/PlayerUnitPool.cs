using UnityEngine;
using UnityEngine.Pool;

public class PlayerUnitPool : MonoBehaviour
{
    [SerializeField] private GameObject _playerUnitPrefab;
    public ObjectPool<GameObject> Pool { get; private set; }
    private void Start()
    {
        Pool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_playerUnitPrefab);
        }, unit =>
        {
            unit.gameObject.SetActive(true);
        }, unit =>
        {
            unit.gameObject.SetActive(false);
        }, unit =>
        {
            Destroy(unit);
        }, false, 100, 200);
    }
}
