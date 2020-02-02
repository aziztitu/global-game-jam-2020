using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BasicTools.ButtonInspector;
using UnityEditor;
using UnityEngine;

public class ObjectPlacementManager : SingletonMonoBehaviour<ObjectPlacementManager>
{
    public Transform rearrangePointsRoot;
    public List<Transform> rearrangePoints;

    [Button("Find Placement Points", "FindPlacementPoints")] [SerializeField]
    private bool _btnFindPlacementPoints;

    private Randomizer<Transform> placementPointsRandomizer;

    new void Awake()
    {
        base.Awake();
        placementPointsRandomizer = new Randomizer<Transform>(rearrangePoints);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FindPlacementPoints()
    {
        rearrangePoints.Clear();
        rearrangePoints = rearrangePointsRoot.GetChildrenOnly();

        // TODO: Set the array as dirty to show array as modified in inspector
    }

    public Transform GetRandomPlacementPoint()
    {
        return placementPointsRandomizer.GetRandomItem();
    }
}