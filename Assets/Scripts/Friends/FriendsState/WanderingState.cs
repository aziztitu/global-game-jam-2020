using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingState : StateMachine<FriendModel>.State
{
    [Serializable]
    public class StateData
    {
        public float waypointPlayerMinDistance;
        public float stopDistance;
    }


    private static WanderingState _instance;

    public static WanderingState Instance => _instance ?? (_instance = new WanderingState());

    private WanderingState()
    {
    }

    public void Enter(FriendModel owner, params object[] args)
    {
        FindNewWaypoint(owner);
        owner.animator.SetBool("isWalking", true);
    }

    public void Update(FriendModel owner)
    {
        if (owner.navMeshAgent.remainingDistance <= owner.wanderingStateData.stopDistance)
        {
            owner.stateMachine.SwitchState(IdleState.Instance);
        }
    }

    public void Exit(FriendModel owner)
    {
        owner.animator.SetBool("isWalking", false);
    }

    void FindNewWaypoint(FriendModel owner)
    {
        var possibleTransforms = WaypointManager.Instance.waypoints.Where(transform =>
        {
            float distanceFromPlayer =
                Vector3.Distance(transform.position, PlayerModel.Instance.transform.position);

            return distanceFromPlayer >= owner.wanderingStateData.waypointPlayerMinDistance;
        }).ToList();

        var targetTransform = possibleTransforms.Count > 0
            ? possibleTransforms[Random.Range(0, possibleTransforms.Count)]
            : WaypointManager.Instance.GetRandomWaypoint();

        owner.navMeshAgent.SetDestination(targetTransform.position);
    }
}