using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FixObjectsTask : Task
{
    public List<FixableObject> allFixableObjects { get; private set; } = new List<FixableObject>();
    public int objectsNotFixed { get; private set; } = 0;

    public override void CalculateTaskStatus()
    {
        base.CalculateTaskStatus();

        objectsNotFixed = 0;
        foreach (var fixableObject in allFixableObjects)
        {
            if (fixableObject.isBroken)
            {
                objectsNotFixed += 1;
            }
        }
    }

    public void AddFixableObject(FixableObject fixableObject)
    {
        allFixableObjects.Add(fixableObject);
    }
}
