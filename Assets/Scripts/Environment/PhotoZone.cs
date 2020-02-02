using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PhotoZone : MonoBehaviour
{
    public List<Transform> camAngleTransforms;
    public int resWidth = 512;
    public int resHeight = 512;

    public Sprite[] images;
    public string text;

    private Texture2D[] pictures;

    public void Awake()
    {
        TakePictures();
    }

    private void TakePictures()
    {
        var camera = LevelManager.Instance.housePictureCamera;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        camera.enabled = true;

        images = new Sprite[camAngleTransforms.Count];
        pictures = new Texture2D[camAngleTransforms.Count];

        for (int i=0; i<camAngleTransforms.Count; i++)
        {
            var camPosition = camAngleTransforms[i];

            camera.transform.position = camPosition.position;
            camera.transform.rotation = camPosition.rotation;

            var picture = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            picture.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            picture.Apply();
            pictures[i] = picture;

            Sprite sprite = Sprite.Create(picture, new Rect(0, 0, resWidth, resHeight), new Vector2(0, 0));
            images[i] = sprite;
        }

        camera.enabled = false;
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("Trigger");
            Gallery.Instance.NewPhotoSet(images, text);
        }
    }
}
