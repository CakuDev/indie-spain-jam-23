using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLifeController : MonoBehaviour
{
    public float maxLife;
    [SerializeField]
    private float realLife;

    public float timeBetweenUpdates;
    [SerializeField]
    private float timeCounter;

    //Coeficientes para calcular la cantidad de daño restado a la luz del faro, en unidades de daño
    public float yellowCof;
    public float redCof;
    public float brokenCof;
    public float brokenCof2;

    //Visuals changes in the Lightbulb
    public SpriteRenderer lightSprite;
    public Sprite lightGreen;
    public Sprite lightYellow;
    public Sprite lightRed;
    public Sprite lightBroken;

    //Lightbulb repairing section
    public bool damagedLightbulb; //Checks if the lightbulb is broken
    public bool hasLightbulb; //Bool to check if the character has a lightbulb to change the damaged one
    public TimeAliveController timeController;

    public RepairableItemAttackController attackController;

    private void Start()
    {
        realLife = maxLife;
        damagedLightbulb = false;
    }

    private void Update()
    {
        if (!damagedLightbulb)
        {
            if (timeCounter >= timeBetweenUpdates)
            {
                CheckHealth();
                timeCounter = 0;
            }
            timeCounter += Time.deltaTime;
        }         
    }

    private void CheckHealth()
    {
        int yellowStatus = 0;
        int redStatus = 0;
        int brokenStatus = 0;

        //Calculamos el número de objetos con cada estado
        List<RepairableItemBehaviour> items = attackController.repairableItems;
        foreach (RepairableItemBehaviour item in items)
        {
            if (item.status == ItemStatus.YELLOW) yellowStatus += 1;
            else if(item.status == ItemStatus.RED) redStatus += 1;
            else if (item.status == ItemStatus.BROKEN) brokenStatus += 1;
        }
        //Sólo si hay algún item estropeado haremos daño
        if (yellowStatus + redStatus + brokenStatus > 0)
        {
            float totalDamage = CalculateDamage(yellowStatus, redStatus, brokenStatus);
            DamageLight(totalDamage);
            //Visual changes and End Game Sequence if life = 0
            CheckStatus();
        } 
    }

    //Ej: Amarillo 0.2 Rojo 0.3 Roto:1 , 0.1 
    public float CalculateDamage(int yellow, int red, int broken)
    {
        float fixedSum = yellow * yellowCof + red * redCof;
        float brokenSum = broken * brokenCof - brokenCof2 * (broken -1);
        return fixedSum + brokenSum;
    }

    //Aparte, por si en algún momento queremos romperlo aparte
    public void DamageLight(float damageQuantity)
    {
        realLife -= damageQuantity;
    }

    public void CheckStatus()
    {
        float lifePercentage = realLife / maxLife * 100;

        if (lifePercentage > 66.6f && lifePercentage <= 100)
        {
            lightSprite.sprite = lightGreen;
        }                    
        else if (lifePercentage > 33.3f && lifePercentage <= 66.6f)
        {
            lightSprite.sprite = lightYellow;
        }
        else if (lifePercentage > 0 && lifePercentage <= 33.3f)
        {
            lightSprite.sprite = lightRed;
        }
        else
        {
            BreakLightBulb();
        }
    }

    public void BreakLightBulb()
    {
        damagedLightbulb = true;
        Debug.Log("Broken Lightbulb Sequence Starts");
        lightSprite.sprite = lightBroken;
        //Aquí llamar a time controller para mostrar indicador de bombilla rota
        timeController.LightbulbBroken();
    }

    //Script called from LightbulbPlacer OnInteract, when having equipped a lightbulb
    public void RestoreLightBulb()
    {
        timeController.LightbulbRestored();
        hasLightbulb = false;
        realLife = maxLife;
        CheckStatus();
        damagedLightbulb = false;
    }

}
