using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendRagdoll : MonoBehaviour
{
    public Transform hip;

    public Pickable pickable;
    public FriendModel friendModelToRevive;

    // Start is called before the first frame update
    void Start()
    {
        pickable = GetComponent<Pickable>();
    }

    // Update is called once per frame
    void Update()
    {
        hip.localPosition = Vector3.zero;
    }
}
