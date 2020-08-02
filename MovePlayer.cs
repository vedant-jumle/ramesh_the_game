using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
	[SerializeField]private CharacterController2D controller;
	[SerializeField]private Animator animator;
	public float speed = 40f;
	private float horizontalMove = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
    	horizontalMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        controller.Move(horizontalMove,Input.GetButtonDown("Crouch"),Input.GetButtonDown("Jump"));
    }

    void Update()
    {
    	updateAnimation();
    }

    private void updateAnimation()
    {
    	animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    	animator.SetBool("Jump", Input.GetButtonDown("Jump"));
    }
}
