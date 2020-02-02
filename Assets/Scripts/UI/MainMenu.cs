using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour {

    public string LevelToLoad;
    public GameObject mainPanel, creditPanel;
    public PlayableDirector play;


    private void Start()
    {
        Time.timeScale = 1;
        creditPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }

    public void PlayGame ()
    {
        play.Play();
        StartCoroutine("LoadLevel");
     
	}

    public void Back()
    {
        creditPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }

    public void Credits()
    {
        creditPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }
	
	
	public void QuitGame()
    {
        Application.Quit();
	}

    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(LevelToLoad);
    }
}
