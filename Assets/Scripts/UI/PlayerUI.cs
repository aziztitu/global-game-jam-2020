using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI pickableInfoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePickableInfo();
    }

    void UpdatePickableInfo()
    {
        var playerPickController = PlayerModel.Instance.playerPickController;
        if (playerPickController.isFocusedOnPickable)
        {
            pickableInfoText.text = playerPickController.pickableOnFocus.instruction;
        }
        else
        {
            pickableInfoText.text = "";
        }
    }
}
