using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionHelper : SingletonMonoBehaviour<SceneTransitionHelper>
{
    public Dictionary<string, object> data = new Dictionary<string, object>();

    public static SceneTransitionHelper Create()
    {
        if (Instance)
        {
            Instance.Destroy();
        }

        return new GameObject("Scene Transition Helper").AddComponent<SceneTransitionHelper>();
    }

    new void Awake()
    {
        base.Awake();

        if (gameObject.activeInHierarchy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        DestroyImmediate(gameObject);
    }
}
