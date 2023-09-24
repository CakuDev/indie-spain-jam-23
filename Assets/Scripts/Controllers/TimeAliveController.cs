using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAliveController : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo; //Countdown text
    public float gameDuration;//Time that has passed since the game started, it serves to determine when the game reachs the good ending
    public float time;//Time that has passed since the game started, it serves to determine when the game reachs the good ending

    //BROKEN LIGHTBULB SECTION
    public GameObject brokenLightbulbCountdown; //Countdown text
    public TextMeshProUGUI lightbulbCountdownText;

    public float lightbulbCountdownTotalTime;//Time available to change the lightbulb before losing the game.
    private float lightbulbCountdownTimer; //actual countdown of changing the lightbulb
    private bool lightbulbBroken;

    public void Start()
    {
        //NORMAL COUNTDOWN SECTION
        time = gameDuration;
        int minutos = Mathf.Max(Mathf.FloorToInt(time / 60), 0);     //minutes
        int segundos = Mathf.Max(Mathf.FloorToInt(time % 60), 0);    //seconds
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;  //if seconds>10, concatenation
        else textoTiempo.text = minutos + ":" + "0" + segundos; //text build

        //BROKEN LIGHTBULB SECTION
        lightbulbBroken = false;
        lightbulbCountdownTimer = lightbulbCountdownTotalTime;
    }

    public void Update()
    {
        //NORMAL COUNTDOWN SECTION
        time -= Time.deltaTime; //updating current time value

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(time / 60), 0);     //minutes
        int segundos = Mathf.Max(Mathf.FloorToInt(time % 60), 0);    //seconds
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //if seconds>10, concatenation
        else textoTiempo.text = minutos + ":" + "0" + segundos;                             //if seconds<10, we add a 0 for for correct formart, then concatenation

        if (time <= 0)
        {
            NightCompleted();
        }

        //BROKEN LIGHTBULB SECTION
        if (lightbulbBroken)
        {
            lightbulbCountdownTimer -= Time.deltaTime;

            //parseo del tiempo a formato contador
            minutos = Mathf.Max(Mathf.FloorToInt(lightbulbCountdownTimer / 60), 0);     //minutes
            segundos = Mathf.Max(Mathf.FloorToInt(lightbulbCountdownTimer % 60), 0);    //seconds
            if (segundos >= 10) lightbulbCountdownText.text = minutos + ":" + segundos; //if seconds>10, concatenation
            else lightbulbCountdownText.text = minutos + ":" + "0" + segundos;

            if (lightbulbCountdownTimer <= 0)
            {
                GameOver();
            }
        }

    }

    public void LightbulbBroken()
    {
        lightbulbCountdownTimer = lightbulbCountdownTotalTime;
        brokenLightbulbCountdown.SetActive(true);
        lightbulbBroken = true;
    }

    public void LightbulbRestored()
    {
        lightbulbCountdownTimer = lightbulbCountdownTotalTime;
        brokenLightbulbCountdown.SetActive(false);
        lightbulbBroken = false;
    }

    public void GameOver()
    {
        Debug.Log("-----------  GAME OVER ------------");
        SceneManager.LoadScene("GameOver");
    }

    public void NightCompleted()
    {
        Debug.Log("----------- NIGHT COMPLETED ------------");
        SceneManager.LoadScene("NightCompleted");
    }
}
