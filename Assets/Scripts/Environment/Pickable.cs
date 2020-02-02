using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour
{
    public Rigidbody rigidbody { get; private set; }
    public ObjectPlacePoint objectPlacePointInRadius { get; private set; }
    public bool hasObjectPlacePointInRadius => objectPlacePointInRadius != null && !objectPlacePointInRadius.isHoldingObject;

    public string pickUpInstruction = "'Left Click' to pick up";
    public string dropInstruction = "'Right Click' to throw/drop";

    public UnityEvent onPickedUp;
    public UnityEvent onDropped;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPickedUp(PlayerInteractionController playerInteractionController)
    {
        rigidbody.isKinematic = true;
        transform.parent = playerInteractionController.pickableHolder;

        transform.DOKill();
        transform.DOLocalMove(Vector3.zero, playerInteractionController.pickUpMoveDuration).Play();
    }

    public void OnDropped(bool forceDrop = false)
    {
        if (objectPlacePointInRadius && !forceDrop)
        {
            OnPlaced(objectPlacePointInRadius);
        }
        else
        {
            rigidbody.isKinematic = false;
            transform.parent = null;
        }
    }

    public void OnPlaced(ObjectPlacePoint objectPlacePoint)
    {
        rigidbody.isKinematic = true;
        transform.parent = objectPlacePoint.holder;

        transform.DOKill();
        transform.DOLocalMove(Vector3.zero, PlayerModel.Instance.playerInteractionController.pickUpMoveDuration).Play();

        objectPlacePoint.OnPlaced(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectPlacePoint") && objectPlacePointInRadius == null)
        {
            objectPlacePointInRadius = other.GetComponent<ObjectPlacePoint>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ObjectPlacePoint"))
        {
            var objectPlacePoint = other.GetComponent<ObjectPlacePoint>();
            if (objectPlacePoint == objectPlacePointInRadius)
            {
                objectPlacePointInRadius = null;
            }
        }
    }
}