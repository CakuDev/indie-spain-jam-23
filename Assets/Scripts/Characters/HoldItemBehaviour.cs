using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItemBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;

    //[HideInInspector] public HoldableObjectBehaviour objectToHold;
    //[HideInInspector] public HoldableObjectBehaviour holdedObject;
    [HideInInspector] public bool canHold = true;

    public void HoldOrDropItem()
    {
        // TODO
        if (!canHold) return;

        Debug.Log("Hold!");
    }
}
