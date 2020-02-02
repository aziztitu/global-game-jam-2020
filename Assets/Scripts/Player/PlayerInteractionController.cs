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
    public float pickUpMoveDuration = 0.5f;
    public float maxThrowForce = 500f;
    public float throwForceChargeSpeed = 200f;
    public float throwChargeObjectMaxLocalOffset = 3f;

    private float curThrowForce = -1;

    [ReadOnly] public Pickable pickableOnFocus;
    [HideInInspector] public Pickable pickableOnHand { get; private set; } = null;

    [ReadOnly] public FixableObject fixableObjectOnFocus;

    private Vector3 pickableHolderOriginalLocalPos;
    private Vector3 pickableHolderOriginalPos => pickableHolder.parent.TransformPoint(pickableHolderOriginalLocalPos);

    // Start is called before the first frame update
    void Start()
    {
        pickableHolderOriginalLocalPos = pickableHolder.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.Instance.isPaused)
        {
            return;
        }

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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var hitInfo, maxInteractionDistance,
            mask))
        {
            var otherRigidbody = hitInfo.collider.attachedRigidbody;
            if (otherRigidbody)
            {
                var pickable = otherRigidbody.GetComponent<Pickable>();
                var fixableObject = otherRigidbody.GetComponent<FixableObject>();
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
    }

    void RefreshPickableOnHand()
    {
        if (Input.GetButtonDown("Pick"))
        {
            if (isFocusedOnPickable)
            {
                pickableOnHand = pickableOnFocus;
                pickableOnHand.OnPickedUp(this);
            }
            /*else if (isHoldingPickable && pickableOnHand.hasObjectPlacePointInRadius)
            {
                pickableOnHand.OnDropped();
                pickableOnHand = null;
            }*/
        }

        if (isHoldingPickable)
        {
            var cam = CameraRigManager.Instance.brain.OutputCamera;
            
            if (Input.GetButtonDown("Throw"))
            {
                curThrowForce = 0;
            }

            if (curThrowForce >= 0)
            {
                if (Input.GetButton("Throw"))
                {
                    curThrowForce += throwForceChargeSpeed * Time.deltaTime;
                    curThrowForce = Mathf.Clamp(curThrowForce, 0, maxThrowForce);
                }

                if (Input.GetButtonUp("Throw"))
                {
                    pickableOnHand.OnDropped();

                    pickableOnHand.rigidbody.AddForce(cam.transform.forward * curThrowForce);

                    pickableOnHand = null;
                    curThrowForce = -1;
                }
            }

            if (Input.GetButtonDown("Pick"))
            {
                curThrowForce = -1;
            }

            if (isHoldingPickable)
            {
                float localObjectOffset = 0;
                if (curThrowForce >= 0)
                {
                    localObjectOffset =
                        HelperUtilities.Remap(curThrowForce, 0, maxThrowForce, 0, throwChargeObjectMaxLocalOffset);
                }

                pickableHolder.transform.position =
                    pickableHolderOriginalPos - cam.transform.forward * localObjectOffset;
            }
        }
        else
        {
            curThrowForce = -1;
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