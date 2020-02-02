using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachine<FriendModel>.State
{
    public class StateData
    {

    }


    private static IdleState _instance;

    public static IdleState Instance => _instance ?? (_instance = new IdleState());

    private IdleState()
    {
    }

    public void Enter(FriendModel owner, params object[] args)
    {
        owner.stateMachine.SwitchState(WanderingState.Instance);
    }

    public void Update(FriendModel owner)
    {
    }

    public void Exit(FriendModel owner)
    {
    }
}
