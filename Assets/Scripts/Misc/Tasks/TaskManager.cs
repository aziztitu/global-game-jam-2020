using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonMonoBehaviour<TaskManager>
{
    public RearrangeObjectsTask rearrangeObjectsTask;
    public FixObjectsTask fixObjectsTask;
    public TrashCleaningTask trashCleaningTask;

    [Header("Score Config")] public float bestRearrangeLeft = 10f;
    public float rearrangeWeight = 0.5f;
    public float bestUnfixedLeft = 0f;
    public float fixWeight = 0.2f;
    public float bestTrashLeft = 10f;
    public float trashWeight = 0.3f;

    [ReadOnly] public float score;

    public string rank
    {
        get
        {
            if (score > 0.9f)
            {
                return "S";
            }

            if (score > 0.8f)
            {
                return "A";
            }

            if (score > 0.7f)
            {
                return "B";
            }

            if (score > 0.6f)
            {
                return "C";
            }

            if (score > 0.5f)
            {
                return "D";
            }

            return "F";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CalculateTaskStatuses()
    {
        rearrangeObjectsTask.CalculateTaskStatus();
        fixObjectsTask.CalculateTaskStatus();
        trashCleaningTask.CalculateTaskStatus();

        score = 0;
        score += HelperUtilities.Remap(rearrangeObjectsTask.objectsOutOfPlace, 0, bestRearrangeLeft, rearrangeWeight,
            0);
        score += HelperUtilities.Remap(fixObjectsTask.objectsNotFixed, 0, bestUnfixedLeft, fixWeight, 0);
        score += HelperUtilities.Remap(trashCleaningTask.trashNotThrown, 0, bestTrashLeft, trashWeight, 0);

        score = Mathf.Clamp01(score);
    }
}