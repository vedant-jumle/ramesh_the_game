using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;

    void Start()
    {
        time = Time.time;
    }

    void Update()
    {
        if(Time.time > time)
        {
            Destroy(gameObject);
        }
    }
}
