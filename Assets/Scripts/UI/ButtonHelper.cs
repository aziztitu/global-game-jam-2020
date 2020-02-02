using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{
    public AudioClip buttonPressClip;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SoundEffectsManager.Instance.Play(buttonPressClip));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
