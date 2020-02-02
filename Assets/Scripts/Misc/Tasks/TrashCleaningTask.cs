using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrashCleaningTask : Task
{
    public HashSet<TrashObject> allTrashObjects { get; private set; } = new HashSet<TrashObject>();
    public int trashNotThrown { get; private set; } = 0;

    public override void CalculateTaskStatus()
    {
        base.CalculateTaskStatus();

        trashNotThrown = allTrashObjects.Count;
    }

    public void AddTrashObject(TrashObject trashObject)
    {
        allTrashObjects.Add(trashObject);
    }

    public void OnTrashedObject(TrashObject trashObject)
    {
        allTrashObjects.Remove(trashObject);
    }
}
