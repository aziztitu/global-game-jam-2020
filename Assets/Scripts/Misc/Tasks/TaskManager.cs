using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonMonoBehaviour<TaskManager>
{
    public RearrangeObjectsTask rearrangeObjectsTask;
    public FixObjectsTask fixObjectsTask;
    public TrashCleaningTask trashCleaningTask;
    public HideFriendsTask hideFriendsTask;

    [Header("Score Config")] public int bestRearrangeLeft = 10;
    public int worstRearrangeLeft = 25;
    public float rearrangeWeight = 0.5f;

    public int bestUnfixedLeft = 0;
    public int worstUnfixedLeft = 2;
    public float fixWeight = 0.2f;

    public int bestTrashLeft = 10;
    public int worstTrashLeft = 25;
    public float trashWeight = 0.3f;

    public int bestFriendsLeft = 2;
    public int worstFriendsLeft = 6;
    public float friendsWeight = 0.3f;

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
        hideFriendsTask.CalculateTaskStatus();

        score = 0;
        score += HelperUtilities.Remap(rearrangeObjectsTask.objectsOutOfPlace, bestRearrangeLeft, worstRearrangeLeft, rearrangeWeight,
            0);
        score += HelperUtilities.Remap(fixObjectsTask.objectsNotFixed, bestUnfixedLeft, worstUnfixedLeft, fixWeight, 0);
        score += HelperUtilities.Remap(trashCleaningTask.trashNotThrown, bestTrashLeft, worstTrashLeft, trashWeight, 0);
        score += HelperUtilities.Remap(hideFriendsTask.friendsNotHidden, bestFriendsLeft, worstFriendsLeft, friendsWeight, 0);

        score = Mathf.Clamp01(score);
    }
}