using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    public Animator lightAnimator;
    public GameObject optionsScreen;
    public GameObject buttonsMenu;
    public GameObject creditsScreen;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OptionsScreenOpen()
    {
        buttonsMenu.SetActive(false);
        lightAnimator.Play("LuzFaro");
        StartCoroutine(WaitForLightOptionsOpen());
    }

    public void OptionsScreenClose()
    {
        optionsScreen.SetActive(false);
        lightAnimator.Play("LuzFaroAlReves");
        StartCoroutine(WaitForLightOptionsClose());
    }

    public void CreditsOpen()
    {
        buttonsMenu.SetActive(false);
        lightAnimator.Play("LuzFaro");
        StartCoroutine(WaitForLightCreditsOpen());
    }

    public void CreditsClose()
    {
        creditsScreen.SetActive(false);
        lightAnimator.Play("LuzFaroAlReves");
        StartCoroutine(WaitForLightCreditsClose());
    }

    //Para opciones
    IEnumerator WaitForLightOptionsOpen()
    {
        yield return new WaitForSeconds(1f);
        optionsScreen.SetActive(true);
    }

    IEnumerator WaitForLightOptionsClose()
    {
        yield return new WaitForSeconds(1f);
        buttonsMenu.SetActive(true);
    }

    //Para creditos
    IEnumerator WaitForLightCreditsOpen()
    {
        yield return new WaitForSeconds(1f);
        creditsScreen.SetActive(true);
    }

    IEnumerator WaitForLightCreditsClose()
    {
        yield return new WaitForSeconds(1f);
        buttonsMenu.SetActive(true);
    }
}
