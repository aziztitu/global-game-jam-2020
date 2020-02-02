using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private ObjectPlacePoint objectPlacePoint;

    void Awake()
    {
        objectPlacePoint = GetComponent<ObjectPlacePoint>();
        objectPlacePoint.onObjectPlaced.AddListener(OnObjectPlaced);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnObjectPlaced()
    {
        if (objectPlacePoint.isHoldingObject)
        {
            var trashObject = objectPlacePoint.holdingObject.GetComponent<TrashObject>();
            if (trashObject)
            {
                TaskManager.Instance.trashCleaningTask.OnTrashedObject(trashObject);
            }

            Destroy(objectPlacePoint.holdingObject.gameObject);
        }
    }
}
