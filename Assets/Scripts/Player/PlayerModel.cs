using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : SingletonMonoBehaviour<PlayerModel>
{
    public PlayerInteractionController playerInteractionController { get; private set; }

    new void Awake()
    {
        base.Awake();

        playerInteractionController = GetComponent<PlayerInteractionController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
