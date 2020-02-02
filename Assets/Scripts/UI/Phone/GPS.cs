using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GPS : MonoBehaviour
{
    public Slider distanceGraphic;
    public TextMeshProUGUI kmText;

    private void Update()
    {
        kmText.text = $"{LevelManager.Instance.curParentsDistance:##0.00} KM";
        distanceGraphic.value = 1 + -LevelManager.Instance.curParentsDistanceNormalized;
    }
}
