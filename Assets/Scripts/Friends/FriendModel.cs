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

    public float minVelocityToGetHit;

    public GameObject ragdollPrefab;

    public bool isHidden = false;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<FriendModel>(this);
        stateMachine.SwitchState(IdleState.Instance);

        TaskManager.Instance.hideFriendsTask.AddFriend(this);
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

    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject);

        if (other.rigidbody)
        {
            var pickable = other.rigidbody.GetComponent<Pickable>();
            if (pickable && other.rigidbody.velocity.magnitude >= minVelocityToGetHit)
            {
                // Gets hit
                // Instantiate ragdoll
                Instantiate(ragdollPrefab, transform.position, transform.rotation).GetComponent<FriendRagdoll>();
            }
        }
    }
}
