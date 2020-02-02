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

    public GameObject avatar;
    public Animator animator;
    public GameObject ragdollPrefab;

    public bool isHidden = false;
    public FriendRagdoll friendRagdoll = null;

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
                print(other.rigidbody.velocity);

                // Gets hit
                // Instantiate ragdoll
                friendRagdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation)
                    .GetComponentInChildren<FriendRagdoll>();

                friendRagdoll.friendModelToRevive = this;

                gameObject.SetActive(false);
            }
        }
    }

    public void Revive()
    {
        transform.position = friendRagdoll.transform.position;

        friendRagdoll = null;

        gameObject.SetActive(true);

        if (!navMeshAgent.isOnNavMesh)
        {
            transform.position = WaypointManager.Instance.GetRandomWaypoint().position;
        }

        stateMachine.SwitchState(IdleState.Instance);
    }
}