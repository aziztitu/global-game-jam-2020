using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhotoZone : MonoBehaviour
{
    public Sprite[] images;
    public string text;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Gallery.Instance.NewPhotoSet(images, text);
        }
    }
}
