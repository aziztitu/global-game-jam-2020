using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableObject : MonoBehaviour
{
    public bool isFixed => fixedVersion.gameObject.activeInHierarchy;
    public bool isBroken => !isFixed;

    public string fixInstruction = "'Left Click' to fix";
    public Transform fixedVersion;
    public Transform brokenVersion;

    void Awake()
    {
        Fix();  // As a precaution
        TaskManager.Instance.fixObjectsTask.AddFixableObject(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Break();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        fixedVersion.gameObject.SetActive(false);
        brokenVersion.gameObject.SetActive(true);
    }

    public void Fix()
    {
        fixedVersion.gameObject.SetActive(true);
        brokenVersion.gameObject.SetActive(false);
    }
}
