using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

public class EnableUnityServices : MonoBehaviour
{
    private void Awake()
    {
        UnityServices.InitializeAsync();
    }
}
