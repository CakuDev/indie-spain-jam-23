using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    public Animator lightAnimator;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OptionsScreenOpen()
    {
        lightAnimator.Play("LuzFaro");
    }

    public void OptionsScreenClose()
    {
        lightAnimator.Play("LuzFaroAlReves");
    }
}
