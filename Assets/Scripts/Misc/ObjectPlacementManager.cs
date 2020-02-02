using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BasicTools.ButtonInspector;
using UnityEditor;
using UnityEngine;

public class ObjectPlacementManager : SingletonMonoBehaviour<ObjectPlacementManager>
{
    public Transform placementPointsRoot;
    public bool findPlacementPointsOnAwake = true;
    public List<Transform> placementPoints;

    private Randomizer<Transform> placementPointsRandomizer;

    new void Awake()
    {
        base.Awake();

        if (findPlacementPointsOnAwake)
        {
            FindPlacementPoints();
        }
        placementPointsRandomizer = new Randomizer<Transform>(placementPoints);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FindPlacementPoints()
    {
        if (placementPointsRoot)
        {
            placementPoints.Clear();
            placementPoints = placementPointsRoot.GetChildrenOnly();
        }
    }

    public Transform GetRandomPlacementPoint()
    {
        return placementPointsRandomizer.GetRandomItem();
    }
}