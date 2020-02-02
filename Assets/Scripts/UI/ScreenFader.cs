using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : SingletonMonoBehaviour<ScreenFader>
{
    public Image fadeImage;

    public bool fadeInOnStart = true;

    public float defaultFadeDuration = 3f;

    new void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fadeInOnStart)
        {
            FadeIn(defaultFadeDuration);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeIn(float duration = -1, TweenCallback callback = null)
    {
        if (duration < 0)
            duration = defaultFadeDuration;

        fadeImage.gameObject.SetActive(true);

        fadeImage.DOKill();
        fadeImage.DOFade(1, 0).SetUpdate(true).OnComplete(() =>
        {
            fadeImage.DOFade(0, duration).SetUpdate(true).OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                callback?.Invoke();
            }).Play();
        }).Play();
    }

    public void FadeOut(float duration = -1, TweenCallback callback = null)
    {
        if (duration < 0)
            duration = defaultFadeDuration;

        fadeImage.DOKill();
        fadeImage.DOFade(0, 0).SetUpdate(true).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1, duration).SetUpdate(true).OnComplete(callback).Play();
        }).Play();
    }
}