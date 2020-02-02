using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendModel : MonoBehaviour
{
    public StateMachine<FriendModel> stateMachine;

    void Awake()
    {
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
