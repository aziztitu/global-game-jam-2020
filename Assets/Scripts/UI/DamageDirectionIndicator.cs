using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DamageDirectionIndicator : MonoBehaviour
{
    [HideInInspector] public Vector3 trackingPos;
    public Image indicatorImage;
    public float fadeDuration = 0.5f;
    public float lifeSpan = 3f;

    private float deathTime; 

    void Awake()
    {
        var col = indicatorImage.color;
        col.a = 0;
        indicatorImage.color = col;

        indicatorImage.DOFade(1, fadeDuration);
        ResetTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIndicatorRotation();

        if (Time.time >= deathTime)
        {
            indicatorImage.DOFade(0, fadeDuration).onComplete += () =>
            {
                Destroy(gameObject);
            };
        }
    }

    void UpdateIndicatorRotation()
    {
        var camForward = CameraRigManager.Instance.brain.OutputCamera.transform.forward;
        camForward.y = 0;

        var playerToTarget = trackingPos - PlayerModel.Instance.transform.position;
        playerToTarget.y = 0;

        var angle = Vector3.SignedAngle(playerToTarget, camForward, Vector3.up);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ResetTimer()
    {
        deathTime = Time.time + lifeSpan;
    }
}
