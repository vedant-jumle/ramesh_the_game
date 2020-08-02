using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBarrel : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
    	playerTransform = transform.root;
    }

    void Update()
    {
    	
        Quaternion rotateTo;
        rotateTo = transform.rotation;
        if(playerTransform.localScale.x < 0f)
        {
            rotateTo = new Quaternion(0f,0f,-180f,1f);
        }
        else
        {
            rotateTo = new Quaternion(0f,0f,0f,1f);
        }

        transform.localRotation = rotateTo;
        //Vector3 mousePos;
        //mousePos = Input.mousePosition;
        //mousePos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePos));

        //transform.right = mousePos;
    }
}
