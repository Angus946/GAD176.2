using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastMold : MonoBehaviour
{
    //Call to other Script
    public cameraHead cameraHead;

    void Update()
    {
        if (cameraHead != null)
        {
            transform.position = cameraHead.transform.position + (cameraHead.transform.forward * cameraHead.hit.distance);
            transform.localScale = Vector3.one * cameraHead.sphereRadius;
        }
    }
}