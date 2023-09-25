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
    public ItemStatus status;

    //UI Attributes
    public List<GameObject> lifeLight;
    public GameObject itemGameObject;
    public Animator animator;

    private void Start()
    {
        //We start the game with full life
        life = totalLife;
        status = ItemStatus.GREEN;
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
                lifeLight.ForEach(light => light.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0));
                animator.SetInteger("lifeLevel", 0);
                status = ItemStatus.GREEN;
            } else
            {
                if (lifePercentage == 100)
                {
                    isBroken = true;
                    lifeLight.ForEach(light => light.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0));
                    animator.SetInteger("lifeLevel", 0);
                    status = ItemStatus.GREEN;
                }
            }            
        }
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f && !isBroken)
        {
            //Change Light to yellow color
            lifeLight.ForEach(light => light.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0));
            animator.SetInteger("lifeLevel", 1);
            status = ItemStatus.YELLOW;
        }
        else if (lifePercentage > 0 && lifePercentage <= 33.3f && !isBroken)
        {
            //Change Light to red color
            lifeLight.ForEach(light => light.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0));
            animator.SetInteger("lifeLevel", 2);
            status = ItemStatus.RED;
        }
        else
        {
            //Turn off light and sprite broken
            lifeLight.ForEach(light => light.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0));
            animator.SetInteger("lifeLevel", 3);
            isBroken = true;
            status = ItemStatus.BROKEN;
        }
    }

    public override void OnInteract(InteractBehaviour interactBehaviour)
    {
        ChangeLife(interactBehaviour.healOrDamagingLife);       
    }

}
