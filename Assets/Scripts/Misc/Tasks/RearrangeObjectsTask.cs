using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RearrangeObjectsTask : Task
{
    public int objectsOutOfPlace { get; private set; } = 0;

    public override void CalculateTaskStatus()
    {
        base.CalculateTaskStatus();

        int objectsRearranged = 0;
        foreach (var point in RearrangableObjectManager.Instance.pointsForRearrangingObjects)
        {
            if (point.isHoldingObject)
            {
                var rearrangable = point.holdingObject.GetComponent<RearrangableObject>();
                if (rearrangable && rearrangable.data == point.correctObjectData)
                {
                    objectsRearranged += 1;
                }
            }
        }

        objectsOutOfPlace = RearrangableObjectManager.Instance.pointsForRearrangingObjects.Count - objectsRearranged;
    }
}