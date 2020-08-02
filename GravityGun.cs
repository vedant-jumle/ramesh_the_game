using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : Weapon_base
{
    [Range(0f,0.5f)]public float pullForce;
    public float shootForce;
    public LayerMask layer;
    public float rangeX = 10f;
    public float rangeY = 1f;
    public Collider2D lockPointCollider;
    public Transform muzzel;

    [SerializeField] private Transform lockPoint;
    [SerializeField] private GameObject crosshair;

    private Transform foundInteractable;
    private bool locked;
    private SpriteRenderer currectCrosshair;
    private Transform lockedInteractable;
    private float multiplier = 10000;

    void Start()
    {
        gunId = 1;
        shootForce *= multiplier;
        lockedInteractable = null;
        locked = false;
        foundInteractable = null;
        currectCrosshair = crosshair.GetComponent<SpriteRenderer>();
        currectCrosshair.enabled = false;
        crosshair.GetComponent<Transform>().SetParent(lockPoint);


        socket = GameObject.FindWithTag("GunSocket");
        transform.SetParent(socket.GetComponent<Transform>());
        transform.SetPositionAndRotation(socket.GetComponent<Transform>().position, socket.GetComponent<Transform>().rotation);

        transform.root.GetComponent<PlayerMain>().ToggleAnimation(gunId, transform);
    }

    void Update()
    { 
        if (Input.GetButtonDown("Fire1") && locked && lockedInteractable != null)
        {
            lockedInteractable.GetComponent<InteractableController>().RemoveLock();
            lockedInteractable.GetComponent<InteractableController>().ApplyForce(muzzel.right * shootForce);
            
            locked = false;
            lockedInteractable = null;
        }
        else if(locked && lockedInteractable != null)
        {
            DrawCrosshair(lockedInteractable, currectCrosshair.GetComponent<Transform>(), false);
        }
        else
        {
            try
            {
                foundInteractable = RunOverlap();
            }
            catch (Exception e)
            {
                foundInteractable = null;
            }
            if (foundInteractable != null && !locked)
            {
                DrawCrosshair(foundInteractable, currectCrosshair.GetComponent<Transform>(), true);
                currectCrosshair.enabled = true;
            }
            else
            {
                currectCrosshair.enabled = false;
            }
        }
    }

    Transform RunOverlap()
    {
        Collider2D result;
        try
        {
            result = SortArray(Physics2D.OverlapBoxAll(lockPoint.position, new Vector2(rangeX, rangeY), 0f, layer))[0];
        }
        catch(Exception e)
        {
            result = null;
        }
        return result.GetComponent<Transform>();
    }

    private Collider2D[] SortArray(Collider2D[] array)
    {
        for(int i = 0;i < array.Length - 1&& array.Length != 0;i++)
        {
            for(int j = 0; j < array.Length - i - 1;j++)
            {
                if(Physics2D.Distance(lockPointCollider, array[j]).distance < Physics2D.Distance(lockPointCollider, array[j + 1]).distance)
                {
                    Collider2D temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        return array;
    }

    void DrawCrosshair(Transform point, Transform crosshair, bool checkLock)
    {
        crosshair.position = point.position;

        if(Input.GetButtonDown("Fire1") && !locked && checkLock)
        {
            point.GetComponent<InteractableController>().LockObject(crosshair, muzzel);
            point.GetComponent<InteractableController>().setPullForce(pullForce);
            lockedInteractable = point;
            locked = true;
        }
        
    }
}
