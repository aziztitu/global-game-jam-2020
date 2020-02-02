using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public bool isHoldingPickable => pickableOnHand != null;
    public bool isFocusedOnPickable => pickableOnFocus != null;
    public bool isFocusedOnFixableObject => fixableObjectOnFocus != null;

    public Transform pickableHolder;

    public float maxInteractionDistance = 5;
    public float pickUpMoveDuration;

    [ReadOnly] public Pickable pickableOnFocus;
    [HideInInspector] public Pickable pickableOnHand { get; private set; } = null;

    [ReadOnly] public FixableObject fixableObjectOnFocus;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RefreshPickableOnFocus();
        RefreshPickableOnHand();
        CheckAndFixObject();
    }

    void RefreshPickableOnFocus()
    {
        pickableOnFocus = null;
        fixableObjectOnFocus = null;

        if (isHoldingPickable)
        {
            return;
        }

        int playerMask = LayerMask.NameToLayer("Player");
        int mask = HelperUtilities.GetOpaqueLayerMaskForRaycast();
        mask &= ~(1 << playerMask);

        var cam = CameraRigManager.Instance.brain.OutputCamera;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hitInfo, maxInteractionDistance, mask))
        {
            var pickable = hitInfo.collider.attachedRigidbody.GetComponent<Pickable>();
            var fixableObject = hitInfo.collider.attachedRigidbody.GetComponent<FixableObject>();
            if (pickable)
            {
                pickableOnFocus = pickable;
            }
            else if (fixableObject && fixableObject.isBroken)
            {
                fixableObjectOnFocus = fixableObject;
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

    void CheckAndFixObject()
    {
        if (Input.GetButtonDown("Fix"))
        {
            if (isFocusedOnFixableObject)
            {
                fixableObjectOnFocus.Fix();
            }
        }
    }
}