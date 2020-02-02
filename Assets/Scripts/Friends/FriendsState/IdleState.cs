using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class IdleState : StateMachine<FriendModel>.State
{
    [Serializable]
    public class StateData
    {
        public float minWaitDuration;
        public float maxWaitDuration;
    }


    private static IdleState _instance;

    public static IdleState Instance => _instance ?? (_instance = new IdleState());

    private IdleState()
    {
    }

    public void Enter(FriendModel owner, params object[] args)
    {
        owner.WaitAndExecute(() => { owner.stateMachine.SwitchState(WanderingState.Instance); },
            Random.Range(owner.idleStateData.minWaitDuration, owner.idleStateData.maxWaitDuration));
    }

    public void Update(FriendModel owner)
    {
    }

    public void Exit(FriendModel owner)
    {
    }
}