using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFpsS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 65;
    }
}
