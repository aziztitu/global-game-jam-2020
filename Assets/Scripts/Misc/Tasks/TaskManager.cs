using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonMonoBehaviour<TaskManager>
{
    public RearrangeObjectsTask rearrangeObjectsTask;
    public FixObjectsTask fixObjectsTask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rearrangeObjectsTask.CalculateTaskStatus();
        fixObjectsTask.CalculateTaskStatus();
    }
}
