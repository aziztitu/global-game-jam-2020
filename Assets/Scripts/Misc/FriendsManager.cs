using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FriendsManager : SingletonMonoBehaviour<FriendsManager>
{
    public enum ActionType
    {
        Rearrange,
        Litter,
        MakeAMess,
    }

    public int minCount = 5;
    public int maxCount = 10;
    public GameObject friendPrefab;

    [Header("Actions Config")] public float minActionInterval;
    public float maxActionInterval;
    [Range(0, 1)] public float actionProbability;
    public float actionMinDistanceFromPlayer = 5f;
    public float minRearrangeObjectForce = 100f;
    public float maxRearrangeObjectForce = 200f;

    private float nextActionTime = 0;

    public int friendsCount { get; private set; }

    public event Action<ActionType, Vector3> onFriendActionPerformed;

    new void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnFriends();
        RefreshNextActionTime();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAndPerformActions();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(PlayerModel.Instance.transform.position, actionMinDistanceFromPlayer);
        }
    }

    void SpawnFriends()
    {
        friendsCount = Random.Range(minCount, maxCount);

        for (int i = 0; i < friendsCount; i++)
        {
            var spawnTransform = WaypointManager.Instance.GetRandomWaypoint();
            if (spawnTransform)
            {
                Instantiate(friendPrefab, spawnTransform.position, spawnTransform.rotation);
            }
        }
    }

    void CheckAndPerformActions()
    {
        if (Time.time > nextActionTime)
        {
            if (Random.Range(0f, 1f) <= actionProbability)
            {
                PerformAction(ChooseRandomAction());
            }
        }
    }

    void PerformAction(ActionType actionType)
    {
        switch (actionType)
        {
            case ActionType.Rearrange:
                var possiblePoints = TaskManager.Instance.rearrangeObjectsTask.pointsWithMissingObjects.Where(point =>
                {
                    float distanceFromPlayer =
                        Vector3.Distance(point.transform.position, PlayerModel.Instance.transform.position);

                    return distanceFromPlayer >= actionMinDistanceFromPlayer && point.isHoldingObject;
                }).ToList();

                if (possiblePoints.Count > 0)
                {
                    var selectedPoint = possiblePoints[Random.Range(0, possiblePoints.Count)];
                    var pickable = selectedPoint.holdingObject;
                    pickable.OnDropped(true);

                    onFriendActionPerformed?.Invoke(actionType, pickable.transform.position);

                    pickable.rigidbody.AddForce(Random.onUnitSphere *
                                                Random.Range(minRearrangeObjectForce, maxRearrangeObjectForce));
                }

                break;
            case ActionType.Litter:
                if (TaskManager.Instance.trashCleaningTask.canSpawnTrash)
                {
                    var possibleTransforms = ObjectPlacementManager.Instance.placementPoints.Where(point =>
                    {
                        float distanceFromPlayer =
                            Vector3.Distance(point.position, PlayerModel.Instance.transform.position);

                        return distanceFromPlayer >= actionMinDistanceFromPlayer;
                    }).ToList();

                    if (possibleTransforms.Count > 0)
                    {
                        var selectedTransform = possibleTransforms[Random.Range(0, possibleTransforms.Count)];
                        Instantiate(TaskManager.Instance.trashCleaningTask.GetRandomTrashPrefab(),
                            selectedTransform.position, selectedTransform.rotation);

                        onFriendActionPerformed?.Invoke(actionType, selectedTransform.position);
                    }
                }

                break;
            case ActionType.MakeAMess:
                var possibleFixableObjects = TaskManager.Instance.fixObjectsTask.allFixableObjects.Where(
                    fixableObject =>
                    {
                        float distanceFromPlayer =
                            Vector3.Distance(fixableObject.transform.position, PlayerModel.Instance.transform.position);

                        return distanceFromPlayer >= actionMinDistanceFromPlayer && fixableObject.isFixed;
                    }).ToList();

                if (possibleFixableObjects.Count > 0)
                {
                    var selectedFixableObject = possibleFixableObjects[Random.Range(0, possibleFixableObjects.Count)];
                    selectedFixableObject.Break();

                    onFriendActionPerformed?.Invoke(actionType, selectedFixableObject.transform.position);
                }

                break;
        }

        RefreshNextActionTime();
    }

    ActionType ChooseRandomAction()
    {
        return (ActionType) Random.Range(0, Enum.GetNames(typeof(ActionType)).Length);
    }

    void RefreshNextActionTime()
    {
        nextActionTime = Time.time + Random.Range(minActionInterval, maxActionInterval);
    }
}