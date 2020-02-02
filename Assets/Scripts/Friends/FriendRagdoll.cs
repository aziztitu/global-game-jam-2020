using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendRagdoll : MonoBehaviour
{
    public float delayBeforeSwitch = 10f;

    [ReadOnly] public Pickable pickable;

    [ReadOnly] public FriendModel friendModelToRevive;

    [ReadOnly] public float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        pickable = GetComponent<Pickable>();
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= delayBeforeSwitch)
        {
            if (friendModelToRevive && !pickable.isHeld)
            {
                friendModelToRevive.Revive();
                Destroy(gameObject);
            }
        }
    }
}
