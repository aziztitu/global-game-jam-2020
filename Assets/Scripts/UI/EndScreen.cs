using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndScreen : SingletonMonoBehaviour<EndScreen>
{
    public bool isShowing => canvas.activeInHierarchy;

    public GameObject canvas;
    public float transitionDuration = 0.5f;

    public TaskReportItem rearrangeTaskItem;
    public TaskReportItem fixObjectsTaskItem;
    public TaskReportItem trashCleaningTaskItem;
    public TaskReportItem hideFriendsTaskItem;
    public TextMeshProUGUI rankText;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show()
    {
        Time.timeScale = 0;
        HelperUtilities.UpdateCursorLock(false);

        RefreshTaskReports();
        ShowScreen(canvas);
    }

    void RefreshTaskReports()
    {
        TaskManager.Instance.CalculateTaskStatuses();

        rearrangeTaskItem.value.text = $"{TaskManager.Instance.rearrangeObjectsTask.objectsOutOfPlace}";
        fixObjectsTaskItem.value.text = $"{TaskManager.Instance.fixObjectsTask.objectsNotFixed}";
        trashCleaningTaskItem.value.text = $"{TaskManager.Instance.trashCleaningTask.trashNotThrown}";

        rankText.text = $"Rank: {TaskManager.Instance.rank}";
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

    public void Restart()
    {
        LevelManager.Instance.Restart();
    }

    public void GoToMainMenu()
    {
        LevelManager.Instance.GoToMainMenu();
    }
}