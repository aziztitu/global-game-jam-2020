using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPlacePoint : MonoBehaviour
{
    public Transform holder;
    public Canvas ui;
    public float maxDisplayDistance;
    public string placeInstruction = "'Left Click' to place";

    [ReadOnly] public RearrangableObjectData correctObjectData;

    public bool isHoldingObject => holdingObject != null;
    public Pickable holdingObject => holder.GetComponentInChildren<Pickable>();

    public UnityEvent onObjectPlaced;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDisplayDistance);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTowardsCamera();
        UpdateGraphics();
    }

    void FaceTowardsCamera()
    {
        var cam = CameraRigManager.Instance.brain.OutputCamera;
        ui.transform.LookAt(cam.transform.position);
    }

    void UpdateGraphics()
    {
        var distanceFromPlayer = Vector3.Distance(PlayerModel.Instance.transform.position, transform.position);
        ui.gameObject.SetActive(distanceFromPlayer <= maxDisplayDistance);
    }

    public void OnPlaced(Pickable pickable)
    {
        onObjectPlaced?.Invoke();
    }
}
