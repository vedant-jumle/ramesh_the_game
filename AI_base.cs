using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_base : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Enemy_base parent;
    [SerializeField] private Transform headBone;
    [SerializeField] private ConstrainRotation constrain;

    [SerializeField] private Transform muzzel;
    [SerializeField] private Animator turret;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireResetTime;

    public bool anim;

    private Transform target;

    private float resetAt = 0f;
    private float forgetAt = 0f;
    public float forgetTime = 5f;
    

    void Awake()
    {
        target = null;
    }

    void Update()
    {
        turret.SetBool("Fire", anim);
        if (target != null)
        {
            if(Time.time > forgetAt)
            { target = null; }
            else
            {
                controller.Move(CalculateDirection(target.position) * parent.speed * Time.fixedDeltaTime, false, false);
                UpdateTurret(target.position);
                Shoot();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Player"))
        {
            target = hitInfo.GetComponent<Transform>();
            forgetAt = Time.time + forgetTime;
            Debug.Log("player locked");
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Player"))
        {
            forgetAt = Time.time + forgetTime;
            Debug.Log("player locked");
        }
    }

    float CalculateDirection(Vector2 targetPlayer)
    {
        float result = 0f;

        if(transform.position.x > targetPlayer.x)
        {
            result = -1f;
        }
        else
        {
            result = 1f;
        }
        Debug.Log(result);
        return result;
    }

    void UpdateTurret(Vector2 targetPlayer)
    {
        Transform root = transform.root;

        Vector2 lookAt;
        Vector2 pos = headBone.position;
        lookAt = pos - targetPlayer;

        if (root.localScale.x > 0f)
        {
            lookAt *= -1f;
            
        }

        headBone.right = lookAt;
        //constrain.UpdateConstrain();
    }

    void Shoot()
    {
        if(Time.time > resetAt)
        {
            Instantiate(bullet, muzzel.position, muzzel.rotation);
            turret.SetBool("Fire", true);
            resetAt = Time.time + fireResetTime;
        }

        
    }
}
