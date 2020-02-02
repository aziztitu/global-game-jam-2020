using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDirectionIndicatorManager : SingletonMonoBehaviour<DamageDirectionIndicatorManager>
{
    public enum IndicatorType
    {
        Rearranged,
        Litter,
        Mess,
    }

    public Transform container;
    public GameObject rearrangeIndicatorPrefab;
    public GameObject litterIndicatorPrefab;
    public GameObject messIndicatorPrefab;

    private Dictionary<Transform, DamageDirectionIndicator> trackingTransforms = new Dictionary<Transform, DamageDirectionIndicator>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IndicateDamageFrom(Vector3 pos, IndicatorType type)
    {
        GameObject indicatorPrefab = null;
        switch (type)
        {
            case IndicatorType.Rearranged:
                indicatorPrefab = rearrangeIndicatorPrefab;
                break;
            case IndicatorType.Litter:
                indicatorPrefab = litterIndicatorPrefab;
                break;
            case IndicatorType.Mess:
                indicatorPrefab = messIndicatorPrefab;
                break;
        }

        var damageDirIndicator = Instantiate(indicatorPrefab, container).GetComponent<DamageDirectionIndicator>();
        damageDirIndicator.trackingPos = pos;
    }

    public void StopTrackingTransform(Transform otherTransform)
    {
        trackingTransforms.Remove(otherTransform);
    }
}
