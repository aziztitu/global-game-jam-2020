using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendModel : MonoBehaviour
{
    public NavMeshAgent navMeshAgent { get; private set; }
    public StateMachine<FriendModel> stateMachine;

    public IdleState.StateData idleStateData = new IdleState.StateData();
    public WanderingState.StateData wanderingStateData = new WanderingState.StateData();

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<FriendModel>(this);
        stateMachine.SwitchState(IdleState.Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
