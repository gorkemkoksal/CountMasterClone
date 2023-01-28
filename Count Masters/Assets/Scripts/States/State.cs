using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public abstract class State 
{
    protected readonly int IS_RUNNING_HASH = Animator.StringToHash("IsRunning");
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void FixedTick(float fixedDeltatime);
    public abstract void Exit();
    public abstract void OnTriggerEnter(Collider other);

    public event Action OnAfterAttack;
    public event Action OnLoose;
    protected void FightProcess(Transform target, Transform army, float runningSpeed, bool isEnemy, float deltaTime)
    {
        for (int i = 1; i < army.childCount; i++)
        {
            army.GetChild(i).rotation =
                   Quaternion.Slerp(army.GetChild(i).rotation, Quaternion.LookRotation(GetDirectionToTarget(target, army), Vector3.up), deltaTime * 3f);

            if (target.childCount < 2)
            {
                if (isEnemy)
                {
                    OnLoose?.Invoke();
                    return;
                }
                else
                {
                    OnAfterAttack?.Invoke();
                    return;
                }
            }
            var Distance = target.GetChild(1).position - army.GetChild(i).position;

            if (Distance.magnitude < 2f)
            {
                army.GetChild(i).position = Vector3.Lerp(army.GetChild(i).position,
                    target.GetChild(1).position, deltaTime * runningSpeed);
            }
        }
    }
    protected Vector3 GetDirectionToTarget(Transform target, Transform army)
    {
        return target.transform.position - army.transform.position;
    }
    protected void BoolAnimParameterSetterForAllUnits(Transform transform, int animHash, bool parameterState)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool(animHash, parameterState);
        }
    }
    protected void FormatUnits(Transform transform, float distanceFactor, float radius, bool isEnemy)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
  
            var newPos = new Vector3(x, 0.01f, z);  
            
            if (!isEnemy)
            {
                transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);
                transform.GetChild(i).DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.OutBack);
            }
            else
            {
                transform.GetChild(i).localPosition = newPos; //Enemies cannot be seen by the player when they instantiated so I directly set the positions for enemies.
            }
        }
    }
    protected void CountAndSetCounterText(Transform transform, TextMeshPro unitCounterText)
    {
        var numberOfUnits = transform.childCount - 1;
        unitCounterText.text = numberOfUnits.ToString();
    }
    protected void CreateUnits(int count, GameObject unitPrefab, Quaternion rotation, Transform parent)
    {
        for (int i = 0; i < count; i++)
        {
            UnityEngine.Object.Instantiate(unitPrefab, parent.position, rotation, parent);
        }
    }
}
