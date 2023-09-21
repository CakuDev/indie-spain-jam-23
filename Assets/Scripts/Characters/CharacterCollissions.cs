using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollissions : MonoBehaviour
{
    // Scrip used to detect interactions for player's character

    PlayerController pc;

    //Interaction attributes
    public RepairableItemBehaviour availableRepItem;

    public GameObject floorDoor;


    void Start()
    {
        pc = transform.GetComponent<PlayerController>(); //Both scripts, cc and collissions, should be in the Player GameObject
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            pc.life -= 2f; //Change for base enemy's attack when wxisting attached script
        }
        if (collision.CompareTag("RepairableItem"))
        {
            availableRepItem = collision.transform.GetComponent<RepairableItemBehaviour>();
            pc.canRepairItem = true;
        }
        if (collision.CompareTag("Door"))
        {
            floorDoor = collision.gameObject;
            pc.canChangeFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {      
        if (collision.CompareTag("RepairableItem"))
        {
            availableRepItem = null;
            pc.canRepairItem = false;
        }
        if (collision.CompareTag("Door"))
        {
            floorDoor = null;
            pc.canChangeFloor = false;
        }
    }
}
