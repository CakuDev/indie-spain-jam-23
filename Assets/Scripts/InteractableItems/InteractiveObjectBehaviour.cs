using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObjectBehaviour : MonoBehaviour
{
    public Sprite buttonToPressSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractBehaviour>())
        {
                collision.gameObject.GetComponent<InteractBehaviour>().interactiveObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractBehaviour>())
        {
            collision.gameObject.GetComponent<InteractBehaviour>().interactiveObject = null;
        }
    }

    public abstract void OnInteract(InteractBehaviour interactBehaviour);
}
