using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrashCleaningTask : Task
{
    public int maxTrashCount = 30;
    public List<GameObject> trashPrefabs;

    private Randomizer<GameObject> trashPrefabsRandomizer = null; 

    public bool canSpawnTrash => allTrashObjects.Count < maxTrashCount;

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

    public GameObject GetRandomTrashPrefab()
    {
        if (trashPrefabsRandomizer == null)
        {
            trashPrefabsRandomizer = new Randomizer<GameObject>(trashPrefabs);
        }

        return trashPrefabsRandomizer.GetRandomItem();
    }
}