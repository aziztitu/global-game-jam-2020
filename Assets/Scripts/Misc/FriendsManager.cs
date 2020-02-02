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
            Instantiate(friendPrefab, spawnTransform.position, spawnTransform.rotation);
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

                    // TODO: Maybe add some force on pickable
                }

                break;
            case ActionType.Litter:
                break;
            case ActionType.MakeAMess:
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