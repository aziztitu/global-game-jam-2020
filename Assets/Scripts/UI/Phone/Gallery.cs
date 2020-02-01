using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    public Sprite[] currentImages;
    public GameObject arrowL, arrowR;
    public bool galleryUp = true;
    public GameObject ImageParent;
    public float animTime = 2f;
    private Transform[] ImageObj;
    public TextMeshProUGUI currentNumber;
    private int currentIndex = 0;

    public GameObject ImagePrefab;


    private void Start()
    {
        PopulateGallery();
    }

    public void NewPhotoSet(Image[] set)
    {

    }

    private void PopulateGallery()
    {
        foreach (Sprite image in currentImages)
        {
            //spawn game object
            var imgSpawn = Instantiate(ImagePrefab, ImageParent.transform) as GameObject;
        }


        ImageObj = ImageParent.GetComponentsInChildren<Transform>();

        for (int i = 0; i < ImageObj.Length; i++)
        {
            if (ImageObj[i].GetComponent<Image>() != null)
            {
                ImageObj[i].GetComponent<Image>().sprite = currentImages[i];
            }
        }
    }

    private void Update()
    {

        currentNumber.text = ((currentIndex + 1).ToString() + " / " + (currentImages.Length).ToString());

        if (Input.GetKeyUp(KeyCode.RightArrow) && galleryUp && currentIndex < currentImages.Length - 1)
        {
            NextImage();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && galleryUp && currentIndex != 0)
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
