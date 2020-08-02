using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float life;

    void Start()
    {
        life += Time.time;
    }

    void Update()
    {
        if(Time.time > life)
        {
            Destroy(gameObject);
        }
    }
}
