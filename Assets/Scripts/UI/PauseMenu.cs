using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : SingletonMonoBehaviour<PauseMenu>
{
    public bool isPaused => pauseCanvas.gameObject.activeInHierarchy;

    public Canvas pauseCanvas;
    public float transitionDuration = 0.5f;
    public string mainMenuScene = "Main Menu";

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;

        HelperUtilities.UpdateCursorLock(false);
        ShowScreen(pauseCanvas.gameObject);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        HelperUtilities.UpdateCursorLock(true);
        HideScreen(pauseCanvas.gameObject);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    void ShowScreen(GameObject screen)
    {
        var preSeq = screen.DOFade(0, 0);
        var fadeInSeq = screen.DOFade(1f, transitionDuration);

        preSeq.AppendCallback(() =>
        {
            screen.SetActive(true);
            fadeInSeq.Play();
        });
        preSeq.Play();
    }

    void HideScreen(GameObject screen, Action callback = null)
    {
        screen.SetActive(true);

        var preSeq = screen.DOFade(1, 0);
        var fadeOutSeq = screen.DOFade(0f, transitionDuration);

        fadeOutSeq.AppendCallback(() =>
        {
            screen.SetActive(false);
            callback?.Invoke();
        });

        preSeq.AppendCallback(() => { fadeOutSeq.Play(); });
        preSeq.Play();
    }
}