using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BasicTools.ButtonInspector;
using UnityEditor;
using UnityEngine;

public class RearrangableObjectManager : SingletonMonoBehaviour<RearrangableObjectManager>
{
    public Transform rearrangePointsRoot;
    public List<Transform> rearrangePoints;

    [Button("Find Rearrange Points", "FindRearrangePoints")] [SerializeField]
    private bool _btnFindRearrangePoints;

    public Randomizer<Transform> rearrangePointsRandomizer;

    new void Awake()
    {
        base.Awake();
        rearrangePointsRandomizer = new Randomizer<Transform>(rearrangePoints);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FindRearrangePoints()
    {
        rearrangePoints.Clear();
        rearrangePoints = rearrangePointsRoot.GetChildrenOnly();

        // TODO: Set the array as dirty to show array as modified in inspector
    }

    public Transform GetRandomRearrangePoint()
    {
        return rearrangePointsRandomizer.GetRandomItem();
    }
}