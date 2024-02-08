using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this){
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

}
