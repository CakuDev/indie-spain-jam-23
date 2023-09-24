using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Image lightbulbImage;
    public Sprite lightOn;
    public Sprite lightOff;


    public void OnPointerEnter(PointerEventData eventData)
    {
        lightbulbImage.sprite = lightOn;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        lightbulbImage.sprite = lightOff;
    }
}
