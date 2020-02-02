using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : SingletonMonoBehaviour<Gallery>
{
    public Sprite[] currentImages;
    public GameObject arrowL, arrowR;
    public bool galleryUp = true;
    public GameObject ImageParent;
    public float animTime = 2f;
    private Transform[] ImageObj;
    public TextMeshProUGUI currentNumber;
    public TextMeshProUGUI displayText;
    public string defaultText;
    private int currentIndex = 0;

    public GameObject ImagePrefab;


    private void Start()
    {
        PopulateGallery(defaultText);
    }

    public void NewPhotoSet(Sprite[] imgs, string caption)
    {
        currentIndex = 0;
        currentImages = imgs;
        HelperExtensions.DestroyAllChildren(ImageParent.transform);
        PopulateGallery(caption);

    }

    private void PopulateGallery(string text)
    {
        displayText.text = text;

        var imageList = new List<Transform>();
        foreach (Sprite image in currentImages)
        {
            //spawn game object
            var imgSpawn = Instantiate(ImagePrefab, ImageParent.transform).GetComponent<Image>();
            imgSpawn.sprite = image;
            imageList.Add(imgSpawn.transform);
        }

        ImageObj = imageList.ToArray();

        /*for (int i = 0; i < ImageObj.Length; i++)
        {
            if (ImageObj[i].GetComponent<Image>() != null)
            {
                ImageObj[i].GetComponent<Image>().sprite = currentImages[i];
            }
        }*/
    }

    private void Update()
    {

        currentNumber.text = ((currentIndex + 1).ToString() + " / " + (currentImages.Length).ToString());

        if (Input.GetKeyUp(KeyCode.E) && galleryUp && currentIndex < currentImages.Length - 1)
        {
            NextImage();
        }
        if (Input.GetKeyUp(KeyCode.Q) && galleryUp && currentIndex != 0)
        {
            PrevImage();
        }

        if(currentIndex == 0)
        {
            arrowL.SetActive(false);
        }
        else
        {
            arrowL.SetActive(true);
        }
        if(currentIndex == currentImages.Length - 1)
        {
            arrowR.SetActive(false);
        }
        else
        {
            arrowR.SetActive(true);
        }

        Mathf.Clamp(currentIndex, 0, currentImages.Length);

        
    }

    private void NextImage()
    {
        Debug.Log("Next");
        float distanceToMove = (ImageObj[0].localPosition.x - ImageObj[currentIndex + 1].localPosition.x);
        print(distanceToMove);
        ImageParent.GetComponent<RectTransform>().DOLocalMoveX(distanceToMove, animTime).Play();
        currentIndex = currentIndex + 1;
    }

    private void PrevImage()
    {
        Debug.Log("Prev");
        float distanceToMove = (ImageObj[0].localPosition.x - ImageObj[currentIndex - 1].localPosition.x);
        print(distanceToMove);
        ImageParent.GetComponent<RectTransform>().DOLocalMoveX(distanceToMove, animTime).Play();
        currentIndex = currentIndex - 1;
    }
    



}
