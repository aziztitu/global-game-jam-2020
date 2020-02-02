using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public float curParentsDistanceNormalized =>
        HelperUtilities.Remap(curParentsDistance, 0, initialParentsDistance, 0, 1);

    public float initialParentsDistance = 100;
    [Tooltip("Per Minute")] public float parentsTravelSpeed = 60;
    public float curParentsDistance { get; private set; } = 0;

    new void Awake()
    {
        base.Awake();

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
    }
}