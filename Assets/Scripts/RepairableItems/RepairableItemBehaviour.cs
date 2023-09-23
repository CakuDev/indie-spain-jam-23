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

    public bool isBroken;//To avoid receiving attacks while repairing

    //UI Attributes
    public GameObject lifeLight;
    public GameObject itemGameObject;
    public Sprite aliveSprite;
    public Sprite brokenSprite;

    private void Start()
    {
        //We start the game with full life
        life = totalLife;
        isBroken = false;
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

        if (lifePercentage > 66.6f && lifePercentage <= 100)
        {
            if (!isBroken)
            {
                //Change Light to green color
                lifeLight.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                transform.GetComponent<SpriteRenderer>().sprite = aliveSprite;
            } else
            {
                if (lifePercentage == 100)
                {
                    isBroken = true;
                    lifeLight.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                    transform.GetComponent<SpriteRenderer>().sprite = aliveSprite;
                }
            }            
        }
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f && !isBroken)
        {
            //Change Light to yellow color
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
            transform.GetComponent<SpriteRenderer>().sprite = aliveSprite;
        }
        else if (lifePercentage > 0 && lifePercentage <= 33.3f && !isBroken)
        {
            //Change Light to red color
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            transform.GetComponent<SpriteRenderer>().sprite = aliveSprite;
        }
        else
        {
            //Turn off light and sprite broken
            lifeLight.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            transform.GetComponent<SpriteRenderer>().sprite = brokenSprite;
            isBroken = true;
        }
    }

    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        ChangeLife(interactBehaviour.healOrDamagingLife);       
    }

}
