using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickController : MonoBehaviour
{
    public bool isHoldingPickable => pickableOnHand != null;
    public bool isFocusedOnPickable => pickableOnFocus != null;

    public Transform pickableHolder;
    [ReadOnly] public Pickable pickableOnFocus;
    [HideInInspector] public Pickable pickableOnHand { get; private set; } = null;

    public float maxPickDistance;
    public float pickUpMoveDuration;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RefreshPickableOnFocus();
        RefreshPickableOnHand();
    }

    void RefreshPickableOnFocus()
    {
        pickableOnFocus = null;

        if (isHoldingPickable)
        {
            return;
        }

        int playerMask = LayerMask.NameToLayer("Player");
        int mask = HelperUtilities.GetOpaqueLayerMaskForRaycast();
        mask &= ~(1 << playerMask);

        var cam = CameraRigManager.Instance.brain.OutputCamera;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hitInfo, maxPickDistance, mask))
        {
            var pickable = hitInfo.collider.GetComponent<Pickable>();
            if (pickable)
            {
                pickableOnFocus = pickable;
            }
        }
    }

    void RefreshPickableOnHand()
    {
        if (Input.GetButtonDown("Pick"))
        {
            if (isHoldingPickable)
            {
                pickableOnHand.OnDropped();
                pickableOnHand = null;
            }
            else if (pickableOnFocus)
            {
                pickableOnHand = pickableOnFocus;
                pickableOnHand.OnPickedUp(this);
            }
        }
    }
}