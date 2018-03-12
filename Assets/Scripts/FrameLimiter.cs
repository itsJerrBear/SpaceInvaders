using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimiter : MonoBehaviour {

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        
    }
}
