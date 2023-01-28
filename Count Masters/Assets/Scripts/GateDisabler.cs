using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDisabler : MonoBehaviour
{
    [SerializeField] private BoxCollider gateCollider1;
    [SerializeField] private BoxCollider gateCollider2;
    public void DisableGateColliders()
    {
        gateCollider1.enabled = false;
        gateCollider2.enabled = false;
    }
    //avoid troubles from multiple interactions
}
