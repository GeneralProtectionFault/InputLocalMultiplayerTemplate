using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAlive : MonoBehaviour
{
    public static GameObject sceneManager;


    // Start is called before the first frame update
    private void Awake()
    {
        if (sceneManager == null)
        {
            sceneManager = gameObject;
            DontDestroyOnLoad(sceneManager);
        }
        else
            Destroy(gameObject);
    }
}
