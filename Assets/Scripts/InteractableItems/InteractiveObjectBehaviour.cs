using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObjectBehaviour : MonoBehaviour
{
    public Sprite buttonToPressSprite;

    public GameObject keyToPress;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractBehaviour>())
        {
            collision.gameObject.GetComponent<InteractBehaviour>().interactiveObject = this;

            if (keyToPress != null)
            {
                keyToPress.GetComponent<SpriteRenderer>().sprite = buttonToPressSprite;
                keyToPress.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractBehaviour>())
        {
            collision.gameObject.GetComponent<InteractBehaviour>().interactiveObject = null;
            if (keyToPress != null)
            {
                keyToPress.SetActive(false);
            }
        }
    }

    public abstract void OnInteract(InteractBehaviour interactBehaviour);
}
