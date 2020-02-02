using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonMonoBehaviour<TaskManager>
{
    public RearrangeObjectsTask rearrangeObjectsTask;
    public FixObjectsTask fixObjectsTask;
    public TrashCleaningTask trashCleaningTask;

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
    }
}
