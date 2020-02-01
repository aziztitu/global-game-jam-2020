using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using UnityEngine;

public class RearrangableObject : MonoBehaviour
{
    [SerializeField] [ReadOnly] private Vector3 originalPosition;

    public RearrangableObjectData data;
    public GameObject originalPointPrefab;

    private Pickable pickable;

    void Awake()
    {
        pickable = GetComponent<Pickable>();
        pickable.onPickedUp.AddListener(OnPickedUp);
        pickable.onDropped.AddListener(OnDropped);
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveOriginalPosition();
        Rearrange();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Rearrange()
    {
        transform.position = RearrangableObjectManager.Instance.GetRandomRearrangePoint().position;
    }

    void SaveOriginalPosition(bool spawnOriginalPoint = true)
    {
        originalPosition = transform.position;
        if (spawnOriginalPoint)
        {
            var originalPoint = Instantiate(originalPointPrefab, transform.position, transform.rotation)
                .GetComponent<ObjectPlacePoint>();
            originalPoint.correctObjectData = data;
        }
    }

    void OnPickedUp()
    {

    }

    void OnDropped()
    {
        
    }
}