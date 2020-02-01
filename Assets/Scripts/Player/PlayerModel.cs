using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : SingletonMonoBehaviour<PlayerModel>
{
    public PlayerPickController playerPickController { get; private set; }

    new void Awake()
    {
        base.Awake();

        playerPickController = GetComponent<PlayerPickController>();
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
