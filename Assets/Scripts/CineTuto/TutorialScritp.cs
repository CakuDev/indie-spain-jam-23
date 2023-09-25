using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScritp : MonoBehaviour
{
    public List<GameObject> tutoriales;
    private int index = 0;
    private bool canShowTutorial;

    public void ChangeScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Start()
    {
        tutoriales[index].SetActive(true);
    }

    private void ShowNextTutoR()
    {
        tutoriales[index].SetActive(false);
        index++;
        tutoriales[index].SetActive(true);
    }

    private void ShowNextTutoL()
    {
        tutoriales[index].SetActive(false);
        index--;
        tutoriales[index].SetActive(true);
    }

    public void RightButton()
    {
        if(index < tutoriales.Count-1)
        {
            ShowNextTutoR();
        }
    }

    public void LeftButton()
    {
        if(index > 0)
        {
            ShowNextTutoL();
        }
    }
}
