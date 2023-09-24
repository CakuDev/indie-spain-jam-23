using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackableController : MonoBehaviour
{
    [SerializeField] private int totalLife;
    [SerializeField] protected Animator animator;

    public int currentLife;

    public bool canBeHit = true;
    public FloorController currentFloor;

    protected void ResetLife()
    {
        currentLife = totalLife;
    }

    public void OnHit()
    {
        Debug.Log($"{gameObject.name} MAYBE HIT...");
        if (currentLife == 0 || !canBeHit) return;

        // Decrease life and do something depending on the remaining life
        currentLife--;
        Debug.Log($"HIT {currentLife}!");
        canBeHit = false;
        if (currentLife > 0) ManageHit();
        else ManageDeath();
    }

    protected abstract void ManageHit();

    protected abstract void ManageDeath();

}
