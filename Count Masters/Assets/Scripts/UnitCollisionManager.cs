using UnityEngine;
public class UnitCollisionManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerUnitPool _playerUnitPool;

    private void OnTriggerEnter(Collider other)
    {
        if (_playerUnitPool == null)
        {
            _playerUnitPool = transform.parent.GetChild(0).GetChild(1).GetComponent<PlayerUnitPool>();
        }
        if (other.CompareTag("enemyUnit"))
        {
            Destroy(other.gameObject);
            _playerUnitPool.Pool.Release(gameObject);
            transform.parent = transform.parent.GetChild(0).GetChild(1);
        }
        else if (other.CompareTag("stair"))
        {
            _animator.SetBool("IsRunning", false);
            transform.parent = null;
        }
        else if (other.CompareTag("block"))
        {
            _playerUnitPool.Pool.Release(gameObject);
            transform.parent = transform.parent.GetChild(0).GetChild(1);
        }
    }
}
