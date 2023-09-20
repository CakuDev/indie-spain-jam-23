using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItemBehaviour : MonoBehaviour
{
    //Life attributes
    [SerializeField]
    private float totalLife; //Total amount of second the item has before breaking
    public float life; //Life countdown

    public bool isBeingRepaired;//To avoid receiving attacks while repairing

    //UI Attributes
    public GameObject lifeLight;
    public GameObject itemGameObject;

    private void Start()
    {
        //We start the game with full life
        life = totalLife;
        isBeingRepaired = false;
    }

    public void Attack(float lifeAmount)
    {
        //We start the game with full life
        life -= lifeAmount;
        CheckStatus(lifeAmount);
    }

    public void Repair(float lifeAmount)
    {
        //We start the game with full life
        life += lifeAmount;
        CheckStatus(lifeAmount);
    }

    private void CheckStatus(float lifeAmount)
    {
        float lifePercentage = lifeAmount / totalLife * 100;

        if (lifePercentage > 66.6f && lifePercentage <= 100)
        {
            //Change Light to green color
            Debug.Log("Green");
        }
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f)
        {
            //Change Light to yellow color
            Debug.Log("Yellow");
        }
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f)
        {
            //Change Light to red color
            Debug.Log("Red");
        }
        else
        {
            //Turn off light and sprite broken
            Debug.Log("Broken");
        }
    }
}
