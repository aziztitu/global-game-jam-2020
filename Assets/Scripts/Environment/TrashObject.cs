﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Pickable))]
public class TrashObject : MonoBehaviour
{
    void Awake()
    {
        TaskManager.Instance.trashCleaningTask.AddTrashObject(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
