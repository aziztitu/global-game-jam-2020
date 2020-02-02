using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public float curParentsDistanceNormalized =>
        HelperUtilities.Remap(curParentsDistance, 0, initialParentsDistance, 0, 1);

    public float initialParentsDistance = 100;
    [Tooltip("Per Minute")] public float parentsTravelSpeed = 60;
    public string mainMenuScene = "Main Menu";
    public Camera housePictureCamera;

    public float curParentsDistance { get; private set; } = 0;

    new void Awake()
    {
        base.Awake();

        Time.timeScale = 1;

        HelperUtilities.UpdateCursorLock(true);

        curParentsDistance = initialParentsDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateParentsDistance();
    }

    void UpdateParentsDistance()
    {
        curParentsDistance -= (parentsTravelSpeed / 60f) * Time.deltaTime;
        curParentsDistance = Mathf.Clamp(curParentsDistance, 0, initialParentsDistance);

        if (!EndScreen.Instance.isShowing && curParentsDistance <= 0)
        {
            EndScreen.Instance.Show();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}