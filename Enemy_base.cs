using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_base : MonoBehaviour
{
    [SerializeField] protected CharacterController2D controller;
    [SerializeField] protected AI_base aiController;
    public float speed;

    private float cycles = 2;
    

    protected float travelDirection;

    void Start()
    {
        travelDirection = 0;
    }

    void Update()
    {

    }
}
