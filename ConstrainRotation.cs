using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainRotation : MonoBehaviour
{

    public Transform target;
    public float limit;

    public void UpdateConstrain()
    {
        Quaternion rotation = target.localRotation;
        
        rotation.z = Mathf.Clamp(transform.localRotation.z, -limit, limit);

        target.localRotation = rotation;
    }
}
