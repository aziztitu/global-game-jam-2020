using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Phone : SingletonMonoBehaviour<Phone>
{
    public enum State
    {
        GPS,
        Gallery
    }
    private State currentState;

    public GameObject gpsPanel, galleryPanel;

    private Animator anim;

    public Animator holderAnim;

    public float fadeTime = 1f;

    private bool phoneUp;
    private bool fullScreen;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SwitchState();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            TogglePhone();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            ToggleFullScreen();
        }

        if(phoneUp == false)
        {
            fullScreen = false;
        }
    }

    private void SwitchState()
    {
        if(currentState == State.Gallery)
        {
            currentState = State.GPS;

        }
        else
        {
            currentState = State.Gallery;
        }

        switch (currentState)
        {
            case State.Gallery:
                galleryPanel.DOFade(1, fadeTime).Play();
                gpsPanel.DOFade(0, fadeTime).Play();
                anim.SetTrigger("GalleryPopUp");
                anim.SetTrigger("GPSPopDown");
                //galleryPanel.SetActive(true);
                //gpsPanel.SetActive(false);
                break;
            case State.GPS:
                galleryPanel.DOFade(0, fadeTime).Play();
                gpsPanel.DOFade(1, fadeTime).Play();
                anim.SetTrigger("GPSPopUp");
                anim.SetTrigger("GalleryPopDown");
                //gpsPanel.SetActive(true);
                //galleryPanel.SetActive(false);
                break;
        }
    }

    private void TogglePhone()
    {
        phoneUp = !phoneUp;

        anim.SetBool("PhoneUp", phoneUp);
    }

    private void ToggleFullScreen()
    {
        if(phoneUp != false)
        {
            fullScreen = !fullScreen;
        }

        holderAnim.SetBool("FullScreen", fullScreen);
    }


}
