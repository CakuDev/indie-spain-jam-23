using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableItemAttackController : MonoBehaviour
{
    public List<RepairableItemBehaviour> repairableItems;
    public float attacksPerSecond;
    public float damagePerAttack;
    public float damageVariability;

    //Attacks randomly 
    public void Attack()
    {
        List<RepairableItemBehaviour> unrepairingItems = new List<RepairableItemBehaviour>();

        //To avoid attacking objects being repaired or are already broken, we exclude them from the selection
        foreach (RepairableItemBehaviour item in repairableItems)
        {
            if (!item.isBeingRepaired || item.life > 0)
            {
                unrepairingItems.Add(item);
            }
        }

        //we select item that is going to be attacked randomly, if there are objects available
        if (unrepairingItems.Count > 0) {

            int itemIndex = Random.Range(0, unrepairingItems.Count);

            //we attack the item with damageVariability
            unrepairingItems[itemIndex].ChangeLife(Random.Range(damagePerAttack - damageVariability, damagePerAttack + damageVariability));
        }
        
    }

}
