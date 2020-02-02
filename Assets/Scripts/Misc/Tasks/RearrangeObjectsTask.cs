using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RearrangeObjectsTask : Task
{
    public int objectsOutOfPlace { get; private set; } = 0;

    public HashSet<ObjectPlacePoint> pointsWithMissingObjects { get; private set; } =
        new HashSet<ObjectPlacePoint>();

    public override void CalculateTaskStatus()
    {
        base.CalculateTaskStatus();

        int objectsRearranged = 0;
        foreach (var point in pointsWithMissingObjects)
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

        objectsOutOfPlace = pointsWithMissingObjects.Count - objectsRearranged;
    }

    public void AddPointWithMissingObject(ObjectPlacePoint objectPlacePoint)
    {
        pointsWithMissingObjects.Add(objectPlacePoint);
    }
}