using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : SingletonMonoBehaviour<WaypointManager>
{
    public Transform waypointsRoot;
    public bool findWaypointsOnAwake = true;
    public List<Transform> waypoints;

    private Randomizer<Transform> waypointsRandomizer;

    new void Awake()
    {
        base.Awake();

        if (findWaypointsOnAwake)
        {
            FindPlacementPoints();
        }
        waypointsRandomizer = new Randomizer<Transform>(waypoints);
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
        if (waypointsRoot)
        {
            waypoints.Clear();
            waypoints = waypointsRoot.GetChildrenOnly();
        }
    }

    public Transform GetRandomPlacementPoint()
    {
        return waypointsRandomizer.GetRandomItem();
    }
}
