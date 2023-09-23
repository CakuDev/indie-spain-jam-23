using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItemAttackController : MonoBehaviour
{
    public List<RepairableItemBehaviour> repairableItems;
    public float timeBetweenAttacks;
    public int attacksPerTime;
    public float damagePerAttack;
    public float damageVariability;
    public AudioClip hackSound;
    public Coroutine itemHackingCoroutine;

    [SerializeField]
    private float timeCounter;

    private void Start()
    {
        timeCounter = 0;
    }

    private void Update()
    {
            if (timeCounter >= timeBetweenAttacks)
            {
                Attack();
                timeCounter = 0;
            }
            timeCounter += Time.deltaTime;
    }

    //Attacks randomly 
    public void Attack()
    {
        List<RepairableItemBehaviour> unrepairingItems = new List<RepairableItemBehaviour>();

        //To avoid attacking objects being repaired or are already broken, we exclude them from the selection
        foreach (RepairableItemBehaviour item in repairableItems)
        {
            if (!item.isBroken)
            {
                unrepairingItems.Add(item);
            }
        }

        //we select item that is going to be attacked randomly, if there are objects available
        if (unrepairingItems.Count > 0) {

            int itemIndex = Random.Range(0, unrepairingItems.Count);

            //we attack the item with damageVariability
            float damageQuantity = Mathf.Abs(Random.Range(damagePerAttack - damageVariability, damagePerAttack + damageVariability));
            unrepairingItems[itemIndex].ChangeLife(-1 * damageQuantity);
            Debug.Log("A hacking attack was produced against " + unrepairingItems[itemIndex].name + " of " + damageQuantity);
        }
        
    }



}
