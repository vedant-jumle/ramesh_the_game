using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanglePickupIcons : MonoBehaviour
{
    [SerializeField] private GameObject shield;

    [Range(0f, 5f)] [SerializeField] private float dangleAmount;
    [Range(0f,1f)][SerializeField] private float danglePerUpdate;

    private bool isGoingUp;
    private float dangleLimit;
    private float lowerDangleLimit;
    private float lastUpdate;

    void Start()
    {
        isGoingUp = true;
        dangleLimit = transform.position.y + dangleAmount;
        lowerDangleLimit = transform.position.y - dangleAmount;
        
    }

    void Update()
    {
        Dangle();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Player"))
        {
            if(hitInfo.GetComponent<PlayerMain>().GetActiveSield() == null)
            {
                Transform shieldSocket = hitInfo.GetComponent<PlayerMain>().getShieldSocket();

                GameObject temp = Instantiate(shield, shieldSocket.position, shieldSocket.rotation);
                temp.transform.SetParent(shieldSocket);
                hitInfo.GetComponent<PlayerMain>().SetActiveShield(temp);
                Destroy(gameObject);
            }
        }
    }

    void Dangle()
    {
        UpdateVelocity();


        Vector3 newPosition = transform.position;
        if (isGoingUp)
        {
            newPosition.y += danglePerUpdate;
            newPosition.y = Mathf.Clamp(newPosition.y, lowerDangleLimit, dangleLimit);
            lastUpdate = danglePerUpdate;
        }
        else
        {
            newPosition.y -= danglePerUpdate;
            newPosition.y = Mathf.Clamp(newPosition.y, lowerDangleLimit, dangleLimit);
            lastUpdate = -danglePerUpdate;
        }

        transform.position = newPosition;
    }

    void UpdateVelocity()
    {
        if (transform.position.y == dangleLimit) { isGoingUp = false; }
        else if(transform.position.y == lowerDangleLimit) { isGoingUp = true; }
    }
}
