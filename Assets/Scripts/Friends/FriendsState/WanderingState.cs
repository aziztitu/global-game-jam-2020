using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : StateMachine<FriendModel>.State
{
    [Serializable]
    public class StateData
    {
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
    }

    void FindNewWaypoint(FriendModel owner)
    {
        var targetTransform = WaypointManager.Instance.GetRandomWaypoint();
        owner.navMeshAgent.SetDestination(targetTransform.position);
    }
}
