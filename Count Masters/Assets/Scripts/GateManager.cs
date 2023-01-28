using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro gateText;
    [field:SerializeField] public bool IsMultiply { get; private set; }

    public int RandomNumber { get; private set; }
    void Start()
    {
        if (IsMultiply)
        {
            RandomNumber = Random.Range(2, 4);
            gateText.text = "X" + RandomNumber;
        }
        else
        {
            RandomNumber = Random.Range(5, 80);
            if (RandomNumber % 5 != 0) RandomNumber -= RandomNumber % 5;
            gateText.text = RandomNumber.ToString();
        }
    }
}
