using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicScript : MonoBehaviour
{
    public List<GameObject> textosCarta;

    private int index = 0;
    private bool canShowText = false;

    public GameObject continuar;

    public void Start()
    {
        StartCoroutine(WaitForAnimationToEnd());
    }

    IEnumerator WaitForAnimationToEnd()
    {
        yield return new WaitForSeconds(5.5f);
        canShowText = true;
        textosCarta[index].SetActive(true);
        continuar.SetActive(true);
    }

    private void ShowNextText()
    {
        textosCarta[index].SetActive(false);
        index++;
        textosCarta[index].SetActive(true);
    }

    private void Update()
    {
        if(Input.anyKeyDown && canShowText)
        {
            if(index < textosCarta.Count-1)
            {
                ShowNextText();
            }
            else
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}
