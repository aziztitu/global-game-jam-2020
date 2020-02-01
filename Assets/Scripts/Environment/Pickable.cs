using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public string instruction = "Press 'E' to pick up";

    private Rigidbody rigidbody;

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

    public void OnPickedUp(PlayerPickController playerPickController)
    {
        rigidbody.isKinematic = true;
        transform.parent = playerPickController.pickableHolder;
        transform.DOLocalMove(Vector3.zero, playerPickController.pickUpMoveDuration).Play();
    }

    public void OnDropped()
    {
        rigidbody.isKinematic = false;
        transform.parent = null;
    }

    public void OnPlaced()
    {
        rigidbody.isKinematic = true;
        transform.parent = null;
    }
}
