using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [HideInInspector]public bool isLocked;
    private Rigidbody2D rb;
    private float gravity;
    private Transform targetPos;
    private float pullAmount;


    private int mode;

    void Start()
    {
        mode = 0;
        pullAmount = 0f;
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        isLocked = false;
    }

    void FixedUpdate()
    {
        if(isLocked)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        Vector2 finalPos = new Vector2(0f, 0f);
        Vector2 currentPos = transform.position;
        

        transform.position = targetPos.position;
    }

    public void LockObject(Transform parent, Transform target)
    {
        transform.SetParent(parent);
        Debug.Log("Locked");
        rb.gravityScale = 0f;
        isLocked = true;
        targetPos = target;
    }

    public void RemoveLock()
    {
        transform.SetParent(null);
        Debug.Log("lock removed");
        rb.gravityScale = gravity;
        isLocked = false;
    }

    public void setPullForce(float amount)
    {
        pullAmount = amount;
    }

    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force);
        Debug.Log("force added");
    }

}
