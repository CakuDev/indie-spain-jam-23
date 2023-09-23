using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAliveController : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo; //Countdown text

    public float time;//Time that has passed since the game started, it serves to give score at game over.

    public void Start()
    {
        time = 0;
        int minutos = Mathf.Max(Mathf.FloorToInt(time / 60), 0);     //minutes
        int segundos = Mathf.Max(Mathf.FloorToInt(time % 60), 0);    //seconds
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;  //if seconds>10, concatenation
        else textoTiempo.text = minutos + ":" + "0" + segundos; //text build

    }

    public void Update()
    {
        time += Time.deltaTime; //updating current time value

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(time / 60), 0);     //minutes
        int segundos = Mathf.Max(Mathf.FloorToInt(time % 60), 0);    //seconds
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //if seconds>10, concatenation
        else textoTiempo.text = minutos + ":" + "0" + segundos;                             //if seconds<10, we add a 0 for for correct formart, then concatenation
    }
}
