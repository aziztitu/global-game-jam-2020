using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : StateMachine<FriendModel>.State
{
    public class StateData
    {

    }


    private static WanderingState _instance;

    public static WanderingState Instance => _instance ?? (_instance = new WanderingState());

    private WanderingState()
    {
    }

    public void Enter(FriendModel owner, params object[] args)
    {
    }

    public void Update(FriendModel owner)
    {

    }

    public void Exit(FriendModel owner)
    {
    }
}
