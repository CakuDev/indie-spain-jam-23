using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItemBehaviour : InteractiveObjectBehaviour
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

    public void ChangeLife(float lifeAmount)
    {
        life = life + lifeAmount;
        //We start the game with full life
        if (life < 0)
        {
            life = 0;
        }
        else if (life > totalLife)
        {
            life = totalLife;
        }

        CheckStatus();
    }

    private void CheckStatus()
    {
        float lifePercentage = life / totalLife * 100;
        Debug.Log(lifePercentage + "%");

        if (lifePercentage > 66.6f && lifePercentage <= 100)
        {
            //Change Light to green color
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(0,255,0);
            Debug.Log("Green");
        }
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f)
        {
            //Change Light to yellow color
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
            Debug.Log("Yellow");
        }
        else if (lifePercentage > 0 && lifePercentage <= 33.3f)
        {
            //Change Light to red color
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            Debug.Log("Red");
        }
        else
        {
            //Turn off light and sprite broken
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            Debug.Log("Broken");
        }
    }

    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        ChangeLife(interactBehaviour.healOrDamagingLife);       
    }

}
