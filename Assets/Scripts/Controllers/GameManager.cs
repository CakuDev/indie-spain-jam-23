using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandlePause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
